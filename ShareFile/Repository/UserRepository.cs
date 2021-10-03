using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShareFile.Repository
{
    /// <summary>
    /// 保存用户访问记录
    /// </summary>
    public class UserRepository
    {
        /// <summary>
        /// 记录用户访问权限
        /// </summary>
        // public readonly static Hashtable USER_Repository = new Hashtable();

        /// <summary>
        /// 检查用户是否有当前资源访问权限
        /// </summary>
        /// <param name="session">用户session</param>
        /// <param name="id">资源ID</param>
        /// <returns></returns>
        public bool CheckPower(ISession session, string id)
        {
            string score = session.GetString(id);
            if (string.IsNullOrWhiteSpace(score))
            {
                return false;
            }
            if (DateTime.Now < DateTime.Parse(score))
            {
                return true;
            }
            session.Remove(id);
            return false;
        }


        /// <summary>
        /// 标记用户对当前资源的访问权限
        /// </summary>
        /// <param name="session">用户session</param>
        /// <param name="id">资源标识</param>
        public void SavePower(ISession session, string id)
        {
            // 设置资源有效期为两小时
            session.SetString(id, DateTime.Now.AddHours(2).ToString());
        }
    }
}
