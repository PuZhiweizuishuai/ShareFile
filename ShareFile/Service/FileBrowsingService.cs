using Microsoft.Extensions.Logging;
using ShareFile.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ShareFile.Domain;

namespace ShareFile.Service
{
    public interface IFileBrowsingService
    {
        List<FileInfoDomain> GetFileList(string path);


        List<DriverInfoDomain> GetDriveInfos();
    }

    public class FileBrowsingServiceImpl : IFileBrowsingService
    {
        /// <summary>
        /// 默认路径
        /// </summary>
        public readonly static string RUN_PATH = Directory.GetCurrentDirectory();

        
        private readonly ILogger<FileBrowsingServiceImpl> _logger;


        public FileBrowsingServiceImpl(ILogger<FileBrowsingServiceImpl> logger)
        {
            _logger = logger;
        }


        public List<DriverInfoDomain> GetDriveInfos()
        {
            List<DriverInfoDomain> driveInfos = new List<DriverInfoDomain>();
            DriveInfo[] allDirves = DriveInfo.GetDrives();
            foreach(var driver in allDirves)
            {
                // 如果当前盘可读, 并且是固定的硬盘
                if (driver.IsReady && driver.DriveType == DriveType.Fixed)
                {
                    var domain = new DriverInfoDomain();
                    domain.Name = driver.Name;
                    domain.Size = driver.TotalSize;
                    domain.FreeSize = driver.AvailableFreeSpace;
                    if (driver.TotalSize == 0)
                    {
                        domain.Percentage = 0;
                    }
                    else
                    {
                        domain.Percentage = Math.Round(100 - ((double)driver.AvailableFreeSpace / driver.TotalSize) * 100, 2);
                    }
                    
                    domain.Type = driver.DriveType.ToString();
                    driveInfos.Add(domain);
                }
                
            }

            return driveInfos;
        }

        public List<FileInfoDomain> GetFileList(string path)
        {
            // 处理 Path
            string newPath = PathFilterUtil.PathFilter(RUN_PATH, path);

            _logger.LogInformation(newPath);
            try
            {
                DirectoryInfo dir = new DirectoryInfo(newPath);
                // 读取当前文件目录下的所有文件夹
                DirectoryInfo[] dirs = dir.GetDirectories();
                var fileInfoDomains = new List<FileInfoDomain>();
                foreach(var d in dirs)
                {
                    var file = new FileInfoDomain
                    {
                        Type = 0,
                        Name = d.Name,
                        Size = 0,
                        CreateTime = TimeUtil.GetUnixTime(d.CreationTime),
                        UpdateTime = TimeUtil.GetUnixTime(d.LastWriteTime),
                        Path = d.FullName
                    };
                    fileInfoDomains.Add(file);
                }

                // 读取当前文件目录下的所有文件
                var files = dir.GetFiles();
                foreach (var f in files)
                {
                    var file = new FileInfoDomain
                    {
                        Type = FileUtil.GetFileType(f.Name),
                        Name = f.Name,
                        Size = f.Length,
                        CreateTime = TimeUtil.GetUnixTime(f.CreationTime),
                        UpdateTime = TimeUtil.GetUnixTime(f.LastWriteTime),
                        Path = f.FullName
                    };
                    fileInfoDomains.Add(file);
                }
                return fileInfoDomains;
            }
            catch (Exception e)
            {
                _logger.LogError($"文件路径: {newPath} 请求异常！\n{e.Message}");
                return null;
            }
        }
    }
}
