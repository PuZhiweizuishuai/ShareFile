using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShareFile.Config;
using ShareFile.DB;
using ShareFile.Domain;
using ShareFile.Utils;
using System;

namespace ShareFile.Controllers
{
    [Route("api/web")]
    [ApiController]
    public class WebInfoController : ControllerBase
    {
        private readonly ShareContext Context;

        private readonly ILogger<WebInfoController> _logger;

        public WebInfoController(ShareContext context, ILogger<WebInfoController> logger)
        {
            Context = context;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// 获取网站信息
        /// </summary>
        /// <returns></returns>
        [Route("info")]
        [HttpGet]
        public ResponseDetails WebInfo()
        {
            return ResponseDetails.Ok().Add("data", WebInfoConfig.WebInfo);
        }

        /// <summary>
        /// 远程登录接口
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        public ResponseDetails Login(ReaderKey key)
        {
            if (key.Key == WebInfoConfig.AdminKey)
            {
                HttpContext.Session.SetString("ADMIN", "ADMIN");
                _logger.LogInformation($"IP：{IpUtil.GetUserIP(HttpContext.Request)} 登录成功！");
                return ResponseDetails.Ok();
            }
            _logger.LogWarning($"来自IP：{IpUtil.GetUserIP(HttpContext.Request)} 使用密码：{key.Key} 登录失败！");
            return ResponseDetails.Ok(0, "error!");
        }

        /// <summary>
        /// 更新网站个性化设置
        /// </summary>
        /// <param name="webInfo"></param>
        /// <returns></returns>
        [Route("update")]
        [HttpPost]
        [ServiceFilter(typeof(AuthFilter))]
        public ResponseDetails UpdateInfo(WebInfoModel webInfo)
        {
            webInfo.Id = 1;
            webInfo.UpdateTime = TimeUtil.GetUnixTime(DateTime.Now);
            if (string.IsNullOrWhiteSpace(webInfo.Name))
            {
                return ResponseDetails.Ok(0, "网站名不能为空！");
            }
            // 背景图片判断
            if (!string.IsNullOrWhiteSpace(webInfo.Background))
            {
                if (FileUtil.IMG.Contains(FileUtil.GetFileSuffix(webInfo.Background)))
                {
                    if (!System.IO.File.Exists(webInfo.Background))
                    {
                        return ResponseDetails.Ok(0, "输入的背景图片地址不存在！");
                    }
                } 
                else
                {
                    return ResponseDetails.Ok(0, "输入的背景图片格式错误！");
                }
            }
            // logo
            if (!string.IsNullOrWhiteSpace(webInfo.Background))
            {
                if (FileUtil.IMG.Contains(FileUtil.GetFileSuffix(webInfo.Background)))
                {
                    if (!System.IO.File.Exists(webInfo.Background))
                    {
                        return ResponseDetails.Ok(0, "输入的logo图片地址不存在！");
                    }
                }
                else
                {
                    return ResponseDetails.Ok(0, "输入的logo图片格式错误！");
                }
            }

            if (!string.IsNullOrWhiteSpace(webInfo.BaseUrl))
            {
                if (!FileUtil.IsUrl(webInfo.BaseUrl))
                {
                    return ResponseDetails.Ok(0, "输入的网站域名错误！");
                }
            }

            Context.WebInfo.Update(webInfo);
            Context.SaveChanges();

            WebInfoConfig.WebInfo = webInfo;
            return ResponseDetails.Ok();
        }
    }
}
