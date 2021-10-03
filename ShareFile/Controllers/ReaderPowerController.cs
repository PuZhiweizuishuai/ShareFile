using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShareFile.Domain;
using ShareFile.Repository;
using ShareFile.Service;
using ShareFile.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareFile.Controllers
{
    /// <summary>
    /// 设置文件阅读权限
    /// </summary>
    [Route("api/reader")]
    [ApiController]
    public class ReaderPowerController : ControllerBase
    {
        private readonly ILogger<ReaderPowerController> _logger;

        private readonly UserRepository _userRepository;

        private readonly IShareService _shareService;

        public ReaderPowerController(ILogger<ReaderPowerController> logger,
                                     UserRepository userRepository,
                                     IShareService shareService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _shareService = shareService;
        }


        /// <summary>
        /// 输入密码接口
        /// </summary>
        /// <param name="readerKey"></param>
        /// <returns></returns>
        [Route("file")]
        [HttpPost]
        public ResponseDetails ReaderFlileByKey(ReaderKey readerKey)
        {
            if (_shareService.CanReader(readerKey.FileId, readerKey.Key))
            {
                _userRepository.SavePower(HttpContext.Session, readerKey.FileId);
                return ResponseDetails.Ok();
            }
            return ResponseDetails.Ok(0, "密码错误！");
        }

        [Route("hasAdmin")]
        [HttpGet]
        public ResponseDetails HasAdmin()
        {
            return ResponseDetails.Ok().Add("data", AuthUtil.CheckUserIdentity(HttpContext));
        }
    }
}
