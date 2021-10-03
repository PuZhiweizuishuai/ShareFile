using ShareFile.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareFile.Config
{
    public class WebInfoConfig
    {
        public static WebInfoModel WebInfo { get; set; }

        public readonly static string AdminKey = Guid.NewGuid().ToString("N");

    }
}
