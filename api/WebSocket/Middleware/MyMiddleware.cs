using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebSocket.Middleware
{
    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }
    public class MyMiddleware
    {
        public readonly RequestDelegate request;
        public MyMiddleware(RequestDelegate @delegate)
        {
            request = @delegate;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine(11);
            await request(context);
        }
    }
}
