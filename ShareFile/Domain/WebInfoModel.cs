using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareFile.Domain
{
    public class WebInfoModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BaseUrl { get; set; }

        public string Background { get; set; }

        public string Logo { get; set; }

        public string Info { get; set; }

        public string ShareText { get; set; }

        public long UpdateTime { get; set; }
    }
}
