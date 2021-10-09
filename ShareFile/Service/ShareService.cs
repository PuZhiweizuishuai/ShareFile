using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ShareFile.DB;
using ShareFile.Domain;
using ShareFile.Repository;
using ShareFile.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShareFile.Service
{
    public interface IShareService
    {
        ShareDomain Save(ShareDomain share);


        ShareDomain GetShare(HttpContext context, string key, bool cleanPath);


        PageDomain<ShareDomain> GetShareList(int page, int size, bool isAdmin);


        bool DeleteShare(ShareDomain share);


        bool CanReader(string id, string key);
    }


    public class ShareServiceImpl : IShareService
    {
        private readonly ShareContext Context;

        private readonly ILogger<ShareServiceImpl> _logger;

        private readonly UserRepository _userRepository;

        public ShareServiceImpl(ShareContext context, ILogger<ShareServiceImpl> logger, UserRepository userRepository)
        {
            Context = context;
            _logger = logger;
            _userRepository = userRepository;
        }

        public bool CanReader(string id, string key)
        {
            ShareDomain share = Context.SharesFile.Find(id);
            if (share == null)
            {
                return false;
            }
            if (share.HasKey)
            {
                if (share.Key == key)
                {
                    return true;
                } 
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public bool DeleteShare(ShareDomain share)
        {
            try
            {
                Context.SharesFile.Remove(share);
                Context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        /// <summary>
        /// 获取分享
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public ShareDomain GetShare(HttpContext context, string key, bool IsDown)
        {
            ShareDomain share = Context.SharesFile.Find(key);
            if (share == null)
            {
                return null;
            }
            if (AuthUtil.CheckUserIdentity(context))
            {
                share.CanReader = true;
                return share;
            }

            if (share.ExpirationTime != 0)
            {
                if (share.ExpirationTime < TimeUtil.GetUnixTime(DateTime.Now))
                {
                    return null;
                }
            }
            if (share.HasKey)
            {
                share.CanReader = _userRepository.CheckPower(context.Session, key);
            }
            else
            {
                share.CanReader = true;
            }
            

            if (share.CanReader)
            {
                // 更新浏览量
                if (IsDown)
                {
                    share.DownloadCount++;
                } 
                else
                {
                    share.ViewCount++;
                }
                Context.SharesFile.Update(share);
                Context.SaveChangesAsync();
            } 
            else
            {
                share.Name = "";
            }
            share.Key = "";
            if (!IsDown)
            {
                share.Path = "";
            }
           
            return share;
        }

        public PageDomain<ShareDomain> GetShareList(int page, int size, bool isAdmin)
        {
            

            List<ShareDomain> shareDomains = new List<ShareDomain>();
            // 在分页前先要是用OrderBy或者OrderByDescending对数据进行正序或者倒序然后在skip（）跳过多少条，take（）查询多少条。
            int[] s;
            int count = 0;
            int skip = 0;

            if (isAdmin)
            {
                count = Context.SharesFile.Count();
                s = PageUtil.GetPageAndCount(page, size, count);
                page = s[0];
                size = s[1];
                skip = page * size;
                Context.SharesFile.OrderByDescending(s => s.CreateTime).Skip(skip).Take(size).ToList().ForEach(
                    share =>
                    {
                        shareDomains.Add(share);
                    }
                );
                return new PageDomain<ShareDomain>(shareDomains, page, size, count, PageUtil.GetTotalPages(size, count));
            }
            count = Context.SharesFile.Where(s => s.HasKey.Equals(false)).Count();
            s = PageUtil.GetPageAndCount(page, size, count);
            page = s[0];
            size = s[1];
            skip = page * size;
            Context.SharesFile.OrderByDescending(s => s.CreateTime).Where(s => s.HasKey.Equals(false)).Skip(skip).Take(size).ToList().ForEach(
                share =>
                {
                    share.Path = "";
                    shareDomains.Add(share);
                }
            );
            return new PageDomain<ShareDomain>(shareDomains, page, size, count, PageUtil.GetTotalPages(size, count));
        }

        public ShareDomain Save(ShareDomain share)
        {
            try
            {
                share.Id = Guid.NewGuid().ToString("N");
                share.CreateTime = TimeUtil.GetUnixTime(DateTime.Now);
                if (share.ExpirationTime != 0)
                {
                    if (share.CreateTime > share.ExpirationTime)
                    {
                        return null;
                    }
                }
                Context.SharesFile.Add(share);
                Context.SaveChanges();
                share.Key = "";
                return share;
            }
            catch (Exception e)
            {
                _logger.LogError($"分享 {share.Path} 保存失败,失败原因：\n {e.Message}");
                return null;
            }
        }
    }
}
