using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareFile.Domain
{
    /// <summary>
    /// 读取文件密码
    /// </summary>
    public class ReaderKey
    {
        /// <summary>
        /// 分享文件ID
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Key { get; set; }
    }
}
