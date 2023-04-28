using System;
using System.Collections.Generic;
using System.Text;

namespace eInvoice.Core.Models
{
    public class ValueAddedTaxExemptionReasonDto
    {
        public int reasonId { get; set; }
        public string key { get; set; }
        public string law { get; set; }
        public string article { get; set; }
        public string paragraph { get; set; }
        public string text { get; set; }
        public string freeFormNote { get; set; }
        public string activeFrom {get; set;}
        public string activeTo {get; set;}
        public string category {get; set;}
    }
}
