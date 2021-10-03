using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareFile.Utils
{
    public class PageUtil
    {
        public static int[] GetPageAndCount(int page, int size, int count)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (size > 100 || size < 10)
            {
                size = 20;
            }

            if (page > GetTotalPages(size, count))
            {
                page = count / size + 1;
            }
            int[] s = { page - 1, size };
            return s;
        }

        public static int GetTotalPages(int size, int count)
        {
            if (count % size == 0)
            {
                return count / size;
            }
            return count / size + 1;
        }

    }
}
