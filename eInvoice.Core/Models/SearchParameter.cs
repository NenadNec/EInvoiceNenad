using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eInvoice.Core.Models
{
    public class SearchParameter
    {
        public List<SortItem> sortItems { get; set; }
        public PagingOptions pagingOptions { get; set; }
        public List<Restriction> restrictions { get; set; }
    }
}
