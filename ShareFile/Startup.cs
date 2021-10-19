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
            Console.WriteLine("������������������������������������������������������������������������������������");
            Console.WriteLine("ϵͳ��������Ա���루Զ�̷���ʱʹ�ã���" + WebInfoConfig.AdminKey);
            Console.WriteLine("������Ϊϵͳ������ɣ�ÿ����������仯���벻Ҫй¶�����룬й¶����������ذ�ȫ���⣡");
            Console.WriteLine("������������������������������������������������������������������������������������");
            Console.WriteLine("����������룺http://127.0.0.1:5000 ���ʴ�ϵͳ��");
            Console.WriteLine("\n��������Ϊϵͳ��־��");
            Console.WriteLine("������������������������������������������������������������������������������������");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string dataSource = "Data Source=" + Path.Combine(Directory.GetCurrentDirectory(), "share.db");
            Console.WriteLine("���ݿ��ַ��" + dataSource);
            // Configuration.GetConnectionString("ShareContext)"
            // Console.WriteLine(dataSource);
            services.AddDbContext<ShareContext>(options =>
                options.UseSqlite(dataSource));
            // ������ݿ��쳣ɸѡ��
            services.AddDatabaseDeveloperPageExceptionFilter();

            // �����ڴ滺��
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "Session";
                options.IdleTimeout = TimeSpan.FromHours(2);

            });

            services.AddControllers();
            
            // ע���ļ����ģ��
            services.AddSingleton<IFileBrowsingService, FileBrowsingServiceImpl>();
            // ע�����ģ��
            services.AddTransient<IShareService, ShareServiceImpl>();

            services.AddSingleton<UserRepository>();

            // ע��Ȩ��������
            services.AddScoped(typeof(AuthFilter));

            // ע��������
            // services.AddMvc(option => { option.Filters.Add(typeof(UserFilter)); }) 


            // ��������ͷ�������ȡIPʱ�õ����Ǵ����������IP
            services.Configure<ForwardedHeadersOptions>(options =>
            {  
                // ����������ı�ͷ�е���Ŀ��
                options.ForwardLimit = null;
                // X-Forwarded-For������������й��ڷ�������Ŀͻ��˺ͺ����������Ϣ��X-Forwarded-Proto��ԭ������ֵ (HTTP/HTTPS)  
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                // ���н���ת��ͷ����֪����ĵ�ַ��Χ�� ʹ����������·��ѡ�� (CIDR) ��ʾ���ṩ IP ��Χ��ʹ��CDNʱӦ���
                options.KnownNetworks.Clear();
                // ���н���ת��ͷ����֪����ĵ�ַ�� ʹ�� KnownProxies ָ����ȷ�� IP ��ַƥ�䡣ʹ��CDNʱӦ���
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

            // ����session
            app.UseSession();

            app.UseAuthorization();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // �� Vue Router history ģʽ�£��������ˢ��404���������
            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));
            });


        }
    }
}
