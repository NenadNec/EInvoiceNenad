using System.Collections.Generic;

namespace eInvoice.Core.Models
{
    public class Restriction
    {
        public string field { get; set; }
        public List<string> values { get; set; }
    }
}