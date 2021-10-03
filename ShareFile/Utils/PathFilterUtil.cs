using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShareFile.Utils
{
    public class PathFilterUtil
    {
        /// <summary>
        /// 返回处理过的文件路径
        /// </summary>
        /// <param name="path">用户输入路径</param>
        /// <param name="defaultPath">错误后默认返回路径</param>
        /// <returns>文件路径</returns>
        public static string PathFilter(string defaultPath, string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return defaultPath;
            }
            return Path.Combine(defaultPath, path);
        }
    }
}
