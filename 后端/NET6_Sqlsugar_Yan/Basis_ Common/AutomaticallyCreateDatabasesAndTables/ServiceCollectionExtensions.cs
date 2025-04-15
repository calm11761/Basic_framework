using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Basis__Common
{
    #region
    //安装Microsoft.Extensions.DependencyInjection 包的主要作用是：
    //注册服务：它允许你将接口与实现类进行绑定，定义生命周期（如瞬态 Transient，作用域 Scoped，单例 Singleton）等，并将这些服务添加到服务（IServiceCollection）中。
    //提供依赖注入容器：它提供了 IServiceProvider，这是一个容器，用来解析服务的实例并注入到需要的地方。
    //简化构造函数注入：你可以在类的构造函数中声明依赖项，DI 容器会自动为你注入这些依赖。
    #endregion
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 这个实现了自动化添加服务注册，避免手动写
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // 加载接口层和实现层的程序集
            var interfaceAssembly = Assembly.Load("Basis_IService");
            var serviceAssembly = Assembly.Load("Basis_Service");

            // 找到所有以 Service 结尾的类
            var implementations = serviceAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"))
                .ToList();

            foreach (var impl in implementations)
            {
                // 尝试找到对应的接口，比如 UserService 对应 IUserService
                var interfaceType = interfaceAssembly.GetTypes()
                    .FirstOrDefault(i => i.IsInterface && i.Name == $"I{impl.Name}");

                if (interfaceType != null)
                {
                    // 注册为 scoped 服务
                    services.AddScoped(interfaceType, impl);
                }
            }

            return services;
        }
    }
}
