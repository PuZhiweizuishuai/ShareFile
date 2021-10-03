using Microsoft.EntityFrameworkCore;
using ShareFile.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareFile.DB
{
    /// <summary>
    /// 操作数据库
    /// </summary>
    public class ShareContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public ShareContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ShareDomain> SharesFile { get; set;}


        public DbSet<WebInfoModel> WebInfo { get; set; }


        /// <summary>
        /// 创建数据库表
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShareDomain>().ToTable("shares_file");
            modelBuilder.Entity<WebInfoModel>().ToTable("web_info");
        }
    }
}
