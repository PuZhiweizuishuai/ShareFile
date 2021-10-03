using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShareFile.Domain
{
    /// <summary>
    /// 文件分享对象
    /// </summary>
    public class ShareDomain
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }


        /// <summary>
        /// 过期时间
        /// </summary>
        public long ExpirationTime { get; set; }

        /// <summary>
        /// 是否需要密码
        /// </summary>
        public bool HasKey { get; set; }

        /// <summary>
        /// 访问密码
        /// </summary>
        public string Key { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime { get; set; }


        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 访问次数
        /// </summary>
        public long ViewCount { get; set; }

        /// <summary>
        /// 下载次数
        /// </summary>
        public long DownloadCount { get; set; }


        public int Type { get; set; }

        /// <summary>
        /// 有没有阅读权限
        /// </summary>
        [NotMapped]
        public bool CanReader { get; set; }
    }
}
