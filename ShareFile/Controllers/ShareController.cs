using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareFile.Config;
using ShareFile.Domain;
using ShareFile.Repository;
using ShareFile.Service;
using ShareFile.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShareFile.Controllers
{
    [Route("api/share")]
    [ApiController]
    public class ShareController : ControllerBase
    {
        private readonly IShareService _shareService;

        public readonly UserRepository _userRepository;

        public ShareController(IShareService shareService)
        {
            _shareService = shareService;
        }

        /// <summary>
        /// 创建分享
        /// </summary>
        /// <param name="share">分享对象</param>
        /// <returns>分享结果</returns>
        [Route("save")]
        [HttpPost]
        [ServiceFilter(typeof(AuthFilter))]
        public ResponseDetails Save(ShareDomain share)
        {
            if (share.Path == Directory.GetCurrentDirectory() + @"\share.db")
            {
                return ResponseDetails.Ok(0, "此文件包含程序敏感信息，暂不能分享");
            }
            ShareDomain shareDomain = _shareService.Save(share);
            if (shareDomain == null)
            {
                return ResponseDetails.Ok(0, "保存失败！");
            }
            return ResponseDetails.Ok().Add("data", shareDomain);
        }

        [Route("get")]
        [HttpGet]
        public ResponseDetails GetShare(string id)
        {
            ShareDomain shareDomain = _shareService.GetShare(HttpContext, id, false);
            if (shareDomain == null)
            {
                return ResponseDetails.Ok(0, "该分享不存在或者已过期！");
            }
            return ResponseDetails.Ok().Add("data", shareDomain);
        }

        [Route("list")]
        [HttpGet]
        public ResponseDetails ShareList(int page, int size)
        {
            return ResponseDetails.Ok().Add("data", _shareService.GetShareList(page, size, AuthUtil.CheckUserIdentity(HttpContext)));
        }
    }
}
