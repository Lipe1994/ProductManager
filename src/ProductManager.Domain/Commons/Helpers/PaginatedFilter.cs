using System;
using System.Collections.Generic;

namespace ProductManager.Domain.Commons.Helpers
{
    public class PaginatedFilter
    {
        public PaginatedFilter()
        {

        }

        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 5;
        public string Term { get; set; } = "";
        public bool OnlyIsActive { get; set; } = true;

    }
}

