using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShareFile.Config;
using ShareFile.DB;
using ShareFile.Domain;
using ShareFile.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareFile
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //foreach(string str in args)
            //{
            //    Console.WriteLine(str);
            //}
            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            host.Run();
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="host"></param>
        private static void CreateDbIfNotExists(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ShareContext>();
                context.Database.EnsureCreated();
                WebInfoModel webInfo = context.WebInfo.Find(1);
                if (webInfo == null)
                {
                    webInfo = new WebInfoModel
                    {
                        Id = 1,
                        Name = "文件分享服务",
                        BaseUrl = "",
                        Background = "",
                        Logo = "",
                        Info = "",
                        ShareText = "",
                        UpdateTime = TimeUtil.GetUnixTime(DateTime.Now)
                    };
                    context.WebInfo.Add(webInfo);
                    context.SaveChanges();
                    WebInfoConfig.WebInfo = webInfo;
                } else
                {
                    WebInfoConfig.WebInfo = webInfo;
                }
                // 初始化测试数据
                //for (int i = 0; i < 100; i++)
                //{
                //    ShareDomain share = new ShareDomain();
                //    share.Id = Guid.NewGuid().ToString("N");
                //    share.CreateTime = TimeUtil.GetUnixTime(DateTime.Now);
                //    share.ExpirationTime = 0;
                //    share.Name = $"test-{i}.test";
                //    share.Path = $"test-{i}.test";
                //    share.ViewCount = i;
                //    share.DownloadCount = i;
                //    share.HasKey = false;
                //    context.SharesFile.Add(share);
                //    context.SaveChanges();
                //}

                //Console.WriteLine("数据初始化完成！");
                // DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "不能正确的创建数据库！");
            }
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://*:5000");
                });
    }
}
