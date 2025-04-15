using Basis_IService;
using Basis_Service;
using SqlSugar;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Basis__Common;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//使用SqlSugar
builder.Services.AddScoped<ISqlSugarClient>(s =>
{
    var sqlSugarClient = new SqlSugarClient(new ConnectionConfig()
    {
        ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection"),
        DbType = DbType.SqlServer, // 使用你实际的数据库类型
        IsAutoCloseConnection = true,
        InitKeyType = InitKeyType.Attribute,
    });

    // 测试数据库连接是否成功
    try
    {
        sqlSugarClient.Ado.GetString("SELECT 1");
    }
    catch (Exception ex)
    {
        throw new InvalidOperationException("数据库连接失败", ex);
    }

    return sqlSugarClient;
});
//注册服务层
//builder.Services.AddScoped<IUserService, UserService>(); //手动
builder.Services.AddApplicationServices(); //自动注册服务层 封装方法写在公共类中的AddApplicationServices里


// 添加 CORS 策略 跨域 //放在 builder.Build()之前
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();


// ⭐ 调用初始化方法（创建数据库+建表）
using (var scope = app.Services.CreateScope())
{
    DatabaseInitializer.Initialize(scope.ServiceProvider, builder.Configuration);//不需要创建表就注释掉
}




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll"); // 使用跨域 放在 UseAuthorization 之前

app.MapControllers();

app.Run();
