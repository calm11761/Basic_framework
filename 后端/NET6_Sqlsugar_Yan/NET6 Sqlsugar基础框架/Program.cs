using Basis_IService;
using Basis_Service;
using SqlSugar;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Basis__Common;
using Basis__Common.AutomaticallyCreateDatabasesAndTables;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();



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
        //如果你要修改验证传输方式，那么要修改这里
        policy.WithOrigins("http://localhost:5173/") // ✅ 前端实际访问的地址
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // ✅ 关键点：允许携带 Cookie ， 验证码绑定 session，就必须保留 
    });
});

#region //jwt
builder.Services.AddSingleton<JwtHelper>();


// 添加 JWT 身份验证服务 //下载MICROSOFT.AspNetCore.Authentication.JwtBearer包 版本为6.0.30
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"]; // 从 appsettings.json 读取

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],

        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),

        ValidateLifetime = true, // 验证过期时间
        ClockSkew = TimeSpan.Zero // 默认是 5 分钟，这里设为 0,后端和前端时间不能差一分一秒也
    };
});

// NuGet: Swashbuckle.AspNetCore

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "在下方输入JWT，Bearer后面要加一个空格：Bearer {token}",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});



// 加上授权服务
builder.Services.AddAuthorization();
#endregion

//启用Session
builder.Services.AddSession();

builder.Services.AddDistributedMemoryCache(); // ✅ 内存中的缓存（用于 Session）

builder.Services.AddHttpContextAccessor(); // ✅ 加这个

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

app.UseAuthentication(); // 💥 授权；放在 UseAuthorization 前面


app.UseCors("AllowAll"); // 使用跨域 放在 UseAuthorization 之前

app.UseSession();//放在 UseAuthorization 之前

app.UseAuthorization();



app.MapControllers();

app.Run();
