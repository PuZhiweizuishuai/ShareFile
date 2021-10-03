using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareFile.Domain
{
    public class PageDomain<T>
    {
        public PageDomain(List<T> data, int page, int size, int totalElements, int totalPages) {
            this.Data = data;
            this.Page = page + 1;
            this.Size = size;
            this.TotalElements = totalElements;
            this.TotalPages = totalPages;
        }

        public PageDomain() { }

        public List<T> Data { get; set; }

        public int Size { get; set; }

        public int Page { get; set; }

        public int TotalElements { get; set; }

        public int TotalPages { get; set; }
    }
}
