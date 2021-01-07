using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BlogCoreDomain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BlogCoreDomain.Model;
using BlogCoreInterface.Services;
using BlogCoreImplement.Services;
using BlogCoreInterface.Repository;
using BlogCoreImplement.Repository;
using Microsoft.OpenApi.Models;
using System.IO;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Http;

namespace BlogCoreApi
{
    public class Startup
    {
        private string ApiName { get; set; } = "简书";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // 使用EF
            services.AddDbContext<BlogDBContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("connstr"));
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //启用内存缓存(该步骤需在AddSession()调用前使用)
            //services.AddDistributedMemoryCache();//启用session之前必须先添加内存
            //services.AddSession();
            /*services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(2000);//设置session的过期时间
                options.Cookie.HttpOnly = true;//设置在浏览器不能通过js获得该cookie的值
            });*/

            // 读取jwt配置信息
            services.Configure<TokenManagement>(Configuration.GetSection("tokenManagement"));
            var token = Configuration.GetSection("tokenManagement").Get<TokenManagement>();
            // 使用jwt验证
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters() {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token.Secret)),
                    ValidIssuer = token.Issuer,
                    ValidAudience = token.Audience,
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(0)
                };
            });
            // 注册仓储模式
            services.AddTransient(typeof(IRepository<>),typeof(GenericRepository<>));
            // 注册用户操作服务
            services.AddScoped<IUserService, UserService>();
            // 注册验证服务
            services.AddScoped<IAuthenticateService, AuthenticateService>();


            services.AddStackExchangeRedisCache(option=> {
                option.Configuration = Configuration.GetSection("AppSettings:RedisCaching:Configuration").Value;
                option.InstanceName = Configuration.GetSection("AppSettings:RedisCaching:InstanceName").Value;
            });
            // 添加跨域策略
            services.AddCors(option => {
                option.AddPolicy("CustomCorsPolicy",policy=> {
                    policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                });
            });
            // 关闭默认的验证方式
            services.Configure<ApiBehaviorOptions>(option => {
                option.SuppressModelStateInvalidFilter = true;
            });

            // 添加api文档
            var path = AppDomain.CurrentDomain.BaseDirectory;
            services.AddSwaggerGen(c => {
                
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    // {ApiName} 定义成全局变量，方便修改
                    Version = "V1",
                    Title = $"{ApiName} 接口文档——Netcore 3.1",
                    Description = $"{ApiName} HTTP API V1",
                    Contact = new OpenApiContact { Name = ApiName, Email = "Blog.Core@xxx.com", Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") },
                    License = new OpenApiLicense { Name = ApiName, Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") }
                });
                c.OrderActionsBy(o => o.RelativePath);
                var xmlPath = Path.Combine(path, "ApiDoc.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改

                var xmlModelPath = Path.Combine(path, "ModelDoc.xml");
                c.IncludeXmlComments(xmlModelPath);

                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                #region Token绑定到ConfigureServices
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
                #endregion
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // 在开发环境下开启api文档
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"{ApiName} V1");
                    //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉，如果你想换一个路径，直接写名字即可，比如直接写c.RoutePrefix = "doc";
                    c.RoutePrefix = "doc";
                });
            }

            app.UseCors("CustomCorsPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles("/public");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
