using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductManager.Domain.Commons.Helpers
{
    public class PaginatedList<T>
    {
        public int PageIndex { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public List<T> Items { get; set; }


        public PaginatedList(List<T> source, int pageIndex, int totalCount, int totalPages)
        {

            PageIndex = pageIndex;
            TotalCount = totalCount;
            TotalPages = totalPages;


            Items = source;

        }

    }
}

