using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareFile.Config;
using ShareFile.Domain;
using ShareFile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ShareFile.Controllers
{
    /// <summary>
    /// 文件浏览 API
    /// 权限设置
    /// 仅本地用户可读
    /// 即通过127.0.0.1访问或localhost访问的用户
    /// </summary>
    [Route("api")]
    [ApiController]
    public class FileBrowsingController : ControllerBase
    {

        private readonly IFileBrowsingService _fileBrowsing;

        public FileBrowsingController(IFileBrowsingService fileBrowsing)
        {
            _fileBrowsing = fileBrowsing;
        }

        /// <summary>
        /// 默认加载项目
        /// 显示系统盘符，方便用户选择
        /// </summary>
        /// <returns>可读盘符列表</returns>
        [Route("path/driver")]
        [HttpGet]
        [ServiceFilter(typeof(AuthFilter))]
        public ResponseDetails DriverInfo()
        {
            return ResponseDetails.Ok().Add("data", _fileBrowsing.GetDriveInfos());
        }

        /// <summary>
        /// 显示当前目录下文件列表
        /// </summary>
        /// <param name="path">文件目录</param>
        /// <returns>文件列表</returns>
        [Route("path/list")]
        [HttpGet]
        [ServiceFilter(typeof(AuthFilter))]
        public ResponseDetails FileBrowsing(string path)
        {
            var hashtable = _fileBrowsing.GetFileList(path);
            if (hashtable == null)
            {
                return ResponseDetails.Ok(0, "错误的请求！");
            }
            return ResponseDetails.Ok().Add("data", hashtable);
        }
    }
}
