using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShareFile.Config;
using ShareFile.Domain;
using ShareFile.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

/// <summary>
/// 展示用户IP
/// </summary>
namespace ShareFile.Controllers
{
    /// <summary>
    /// 展示用户IP
    /// </summary>
    [Route("api/ip")]
    [ApiController]
    public class UserIpController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly string host = "";

        private readonly string ip = $"http://{IpUtil.IPV4()}";

        public UserIpController(IConfiguration configuration)
        {
            _configuration = configuration;
            host = _configuration["ShareWebHost"];
        }

        /// <summary>
        /// 返回用户访问IP
        /// </summary>
        /// <returns>返回用户访问IP</returns>
        [Route("user")]
        [HttpGet]
        public ResponseDetails GetIp()
        {
            return ResponseDetails.Ok().Add("data", IpUtil.GetUserIP(Request));
        }

        /// <summary>
        /// 返回系统IP
        /// </summary>
        /// <returns>返回用户访问IP</returns>
        [Route("sys")]
        [HttpGet]
        public ResponseDetails SysIp()
        {
            var sysIP = WebInfoConfig.WebInfo.BaseUrl;
            if (string.IsNullOrWhiteSpace(sysIP))
            {
                sysIP = ip;
            }
            return ResponseDetails.Ok().Add("data", sysIP);
        }
    }
}
