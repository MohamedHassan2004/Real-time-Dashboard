using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Domain.Common
{
    public class PaginationFilter
    {
        public string Filters { get; set; }
        public string Sorts { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
