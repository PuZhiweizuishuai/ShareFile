using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Win32;
using ShareFile.Config;
using ShareFile.DB;
using ShareFile.Repository;
using ShareFile.Service;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Policy;

namespace ShareFile
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Console.WriteLine("——————————————————————————————————————————");
            Console.WriteLine("系统超级管理员密码（远程访问时使用）：" + WebInfoConfig.AdminKey);
            Console.WriteLine("此密码为系统随机生成，每次启动都会变化，请不要泄露此密码，泄露可能造成严重安全问题！");
            Console.WriteLine("——————————————————————————————————————————");
            Console.WriteLine("打开浏览器输入：http://127.0.0.1:5000 访问此系统！");
            Console.WriteLine("\n以下内容为系统日志：");
            Console.WriteLine("——————————————————————————————————————————");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string dataSource = "Data Source=" + Path.Combine(Directory.GetCurrentDirectory(), "share.db");
            Console.WriteLine("数据库地址：" + dataSource);
            // Configuration.GetConnectionString("ShareContext)"
            // Console.WriteLine(dataSource);
            services.AddDbContext<ShareContext>(options =>
                options.UseSqlite(dataSource));
            // 添加数据库异常筛选器
            services.AddDatabaseDeveloperPageExceptionFilter();

            // 开启内存缓存
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "Session";
                options.IdleTimeout = TimeSpan.FromHours(2);

            });

            services.AddControllers();
            
            // 注入文件浏览模块
            services.AddSingleton<IFileBrowsingService, FileBrowsingServiceImpl>();
            // 注入分享模块
            services.AddTransient<IShareService, ShareServiceImpl>();

            services.AddSingleton<UserRepository>();

            // 注入权限拦截器
            services.AddScoped(typeof(AuthFilter));

            // 注入拦截器
            // services.AddMvc(option => { option.Filters.Add(typeof(UserFilter)); }) 


            // 处理请求头，避免获取IP时拿到的是代理服务器的IP
            services.Configure<ForwardedHeadersOptions>(options =>
            {  
                // 限制所处理的标头中的条目数
                options.ForwardLimit = null;
                // X-Forwarded-For：保存代理链中关于发起请求的客户端和后续代理的信息。X-Forwarded-Proto：原方案的值 (HTTP/HTTPS)  
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                // 从中接受转接头的已知网络的地址范围。 使用无类别域际路由选择 (CIDR) 表示法提供 IP 范围。使用CDN时应清空
                options.KnownNetworks.Clear();
                // 从中接受转接头的已知代理的地址。 使用 KnownProxies 指定精确的 IP 地址匹配。使用CDN时应清空
                options.KnownProxies.Clear();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShareFile", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //... and tell Swagger to use those XML comments.
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShareFile v1"));
            }
           
            app.UseRouting();

            // 启用session
            app.UseSession();

            app.UseAuthorization();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // 在 Vue Router history 模式下，避免出现刷新404错误的问题
            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));
            });


        }
    }
}
