using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareFile.Domain
{
    public class FileInfoDomain
    {
        /// <summary>
        /// 文件类型
        /// 0 文件夹
        /// 1 文件
        /// </summary>
        public int Type { get; set; }

        public string Name { get; set; }

        public long Size { get; set; }

        public long CreateTime { get; set; }

        public long UpdateTime { get; set; }

        public string Path { get; set; }
    }
}
