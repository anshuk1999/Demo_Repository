using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.Dto.Listing
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }

        public PaginatedResult(List<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }
    }
}
