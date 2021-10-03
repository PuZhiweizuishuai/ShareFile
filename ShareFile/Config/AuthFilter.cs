using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShareFile.Domain;
using ShareFile.Repository;
using ShareFile.Service;
using ShareFile.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ShareFile.Config
{
    /// <summary>
    /// 接口权限限制
    /// </summary>
    public class AuthFilter : ActionFilterAttribute
    {
        private readonly UserRepository _userRepository;

        public readonly static HashSet<string> WHITE_LIST = new HashSet<string>
        {
            "127.0.0.1",
            "localhost",
            "::1"
        };

        private readonly ILogger<AuthFilter> _logger;

        public AuthFilter(UserRepository userRepository, ILogger<AuthFilter> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (!AuthUtil.CheckUserIdentity(context.HttpContext))
            {
                _logger.LogWarning($"安全报警：\nIP： {IpUtil.GetUserIP(context.HttpContext.Request)} 尝试访问 {context.HttpContext.Request.Path} 接口被系统拦截！");
                context.HttpContext.Response.StatusCode = 200;
                var result = new JsonResult(ResponseDetails.Ok(403, "No Power!"));
                context.Result = result;
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
