using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebSocket.Middleware
{
    public class Demo
    {

    }
    //1.定义并实现业务逻辑
    public interface IMyService
    {
        int prop { get; set; }
    }
    public class MyService : IMyService
    {
        public int prop { get; set; }
    }

    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context,IMyService myService)
        {
            myService.prop = 123;
            Console.WriteLine(11);
            await _next(context);
        }
    }

    //3.1 添加依赖服务注册
    public static partial class CustomMiddlewareExtensions
    {
        /// <summary>
        /// 添加服务的依赖注册
        /// </summary>
        public static IServiceCollection AddCustom(this IServiceCollection services)
        {
            return services.AddScoped<IMyService,MyService>();
        }
    }
    public static partial class CustomMiddlewareExtensions
    {
        /// <summary>
        /// 使用中间件
        /// </summary>
        public static IApplicationBuilder UseCustom(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
