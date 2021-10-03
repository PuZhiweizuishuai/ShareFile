using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using ShareFile.Config;
using ShareFile.Domain;
using ShareFile.Service;
using ShareFile.Utils;
using System;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Net.Http.Json;
using System.Text.Json;

namespace ShareFile.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileViewAndSaveController : ControllerBase
    {

        private readonly ILogger<FileViewAndSaveController> _logger;

        private readonly IShareService _shareService;

        private readonly static FileExtensionContentTypeProvider Provider = new FileExtensionContentTypeProvider();

        public FileViewAndSaveController(ILogger<FileViewAndSaveController> logger,
                                         IShareService shareService)
        {
            _logger = logger;
            _shareService = shareService;
        }

        /// <summary>
        /// 所有文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [Route("load")]
        [HttpGet]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> DownloadFile(string path, string type)
        {
            if (string.IsNullOrEmpty(path))
            {
                return Content("404 for not found!");
            }

            try
            {
                // string range = HttpContext.Request.Headers["Range"];
                // System.Console.WriteLine(range);
                var filePath = PathFilterUtil.PathFilter(Directory.GetCurrentDirectory(), path);
                //var memoryStream = new MemoryStream();
                //using (var stream = new FileStream(filePath, FileMode.Open))
                //{
                //    await stream.CopyToAsync(memoryStream);
                //}
                //memoryStream.Position = 0;
                // 获取文件的ContentType
                string fileExt = Path.GetExtension(path);

                var memi = "application/octet-stream";
                try
                {
                    memi = Provider.Mappings[fileExt];
                } 
                catch (Exception e)
                {
                    memi = "application/octet-stream";
                }

                if (type == "inline")
                {
                    Response.Headers.Add("Content-Disposition", $"inline; filename={System.Net.WebUtility.UrlEncode(Path.GetFileName(filePath))}");
                    return PhysicalFile(filePath, memi, true);
                    //return File(memoryStream, memi, true);
                }
                return PhysicalFile(filePath, memi, Path.GetFileName(filePath), true);
            }
            catch (DirectoryNotFoundException e)
            {
                _logger.LogError($"文件：{path}，没有找到！\n{e.Message}");
                return Content("404 for not found!");
            }
        }


        [Route("share")]
        [HttpGet]
        public async Task<IActionResult> ShareFlie(string id, string type)
        {
            ShareDomain share = _shareService.GetShare(HttpContext, id, true);
            //_logger.LogInformation(share.Path);
            if (share == null)
            {
                return Content("404 for not found!");
            }
            return await DownloadFile(share.Path, type);
        }

        [Route("bg")]
        [HttpGet]
        public async Task<IActionResult> LoadBgAndLogoFile(string type)
        {
            
            if (type == "logo")
            {
                if (string.IsNullOrWhiteSpace(WebInfoConfig.WebInfo.Logo))
                {
                    return Content(JsonSerializer.Serialize(ResponseDetails.Ok(0, "No Logo!")), "application/json; charset=UTF-8", Encoding.UTF8);
                }
                return await DownloadFile(WebInfoConfig.WebInfo.Logo, "inline");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(WebInfoConfig.WebInfo.Background))
                {
                    return Content(JsonSerializer.Serialize(ResponseDetails.Ok(0, "No Background!")), "application/json; charset=UTF-8", Encoding.UTF8);
                }
                return await DownloadFile(WebInfoConfig.WebInfo.Background, "inline");
            }
        }
    }
}
