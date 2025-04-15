using Microsoft.Extensions.Configuration;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Basis__Common
{
    public static class DatabaseInitializer
    {
        /// <summary>
        /// 先检查数据库中是否存在改数据库，如果没有再创建
        /// </summary>  //安装Microsoft.Extensions.Configuration.Abstractions包
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        public static void Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var config = configuration.GetConnectionString("DefaultConnection");
            var dbName = config.Split(';').FirstOrDefault(x => x.StartsWith("Initial Catalog="))?.Split('=')[1];

            // 使用 master 库检测数据库是否存在
            var masterDb = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = config.Replace($"Initial Catalog={dbName}", "Initial Catalog=master"),
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            });

            var exists = masterDb.Ado.GetString($"SELECT DB_ID('{dbName}')");
            if (string.IsNullOrEmpty(exists))
            {
                masterDb.Ado.ExecuteCommand($"CREATE DATABASE [{dbName}]");
            }

            // 连接到目标数据库并执行建表 
            var db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = config,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
            //获取Basis_Model类库下的models
            var modelAssembly = Assembly.Load("Basis_Model");  // 确保使用正确的类库名称
            var modelTypes = modelAssembly.GetTypes()
                .Where(t => t.Namespace == "Basis_Model.Models" && t.IsClass)
                .ToArray();


            if (modelTypes.Any())  // 这里添加一个条件判断是否有找到模型
            {
                db.CodeFirst.InitTables(modelTypes);
                Console.WriteLine("成功创建了以下数据表：");
                foreach (var type in modelTypes)
                {
                    Console.WriteLine($"- 表名：{type.Name}");
                }
            }
            else
            {
                Console.WriteLine("没有找到需要创建的表模型！");
            }
            db.CodeFirst.InitTables(modelTypes); //自动建表
        }
    }
}
