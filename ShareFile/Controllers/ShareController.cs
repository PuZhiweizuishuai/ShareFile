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

        /// <summary>
        /// 删除分享
        /// </summary>
        /// <param name="share">删除对象</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        [ServiceFilter(typeof(AuthFilter))]
        public ResponseDetails Delete(ShareDomain share)
        {
            if (_shareService.DeleteShare(share))
            {
                return ResponseDetails.Ok();
            }
            return ResponseDetails.Ok(0, "删除失败，分享不存在或者已删除！");
        }

        /// <summary>
        /// 获取分享
        /// </summary>
        /// <param name="id">分享ID</param>
        /// <returns></returns>
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


        /// <summary>
        /// 分享列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">每页大小</param>
        /// <returns></returns>
        [Route("list")]
        [HttpGet]
        public ResponseDetails ShareList(int page, int size)
        {
            return ResponseDetails.Ok().Add("data", _shareService.GetShareList(page, size, AuthUtil.CheckUserIdentity(HttpContext)));
        }
    }
}
