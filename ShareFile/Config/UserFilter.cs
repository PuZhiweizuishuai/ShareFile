using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using ShareFile.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareFile.Config
{
    /// <summary>
    /// 为访问用户添加身份
    /// </summary>
    public class UserFilter : ActionFilterAttribute
    {

        private readonly UserRepository _userRepository;

        private readonly IConfiguration _configuration;

        public UserFilter(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// 在 Controller 方法之前执行
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //// 添加用户ID标识
            //var cookie = context.HttpContext.Request.Cookies["TOKEN"];
            //if (cookie == null)
            //{
            //    CookieOptions cookieOptions = new CookieOptions();
            //    cookieOptions.HttpOnly = true;
            //    string key = Guid.NewGuid().ToString("N");
            //    context.HttpContext.Response.Cookies.Append("TOKEN", key, cookieOptions);
                
            //    _userRepository.SaveUser(key);
            //} 
            //else
            //{
            //    if (!_userRepository.HasUser(cookie))
            //    {
            //        _userRepository.SaveUser(cookie);
            //    }
            //}
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// 在 Controller 方法之后执行
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
