using Microsoft.AspNetCore.Http;
using ShareFile.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareFile.Utils
{
    public class AuthUtil
    {
        public static bool CheckUserIdentity(HttpContext context)
        {
            string ip = IpUtil.GetUserIP(context.Request);
            if (!AuthFilter.WHITE_LIST.Contains(ip))
            {
                if (context.Session.GetString("ADMIN") == "ADMIN")
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
    }
}
