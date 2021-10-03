using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareFile.Domain
{
    public class DriverInfoDomain
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public long Size { get; set; }

        public long FreeSize { get; set; }

        public double Percentage { get; set; }
    }
}
