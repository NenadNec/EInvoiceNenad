using System;
using System.Collections.Generic;
using System.Text;
using static eInvoice.Core.Models.Enums;

namespace eInvoice.Core.Models
{
    public class SimplePurchaseInvoiceDto
    {
        public int invoiceId { get; set; }
        public string globUniqId { get; set; }
        public PurchaseInvoiceStatus status { get; set; }
        public string comment { get; set; }
        public CirInvoiceStatus cirStatus { get; set; }
        public string cirInvoiceId { get; set; }
        public int version { get; set; }
        public DateTime lastModifiedUtc { get; set; }
        public double cirSettledAmount { get; set; }
        string vatNumberFactoringCompany { get; set; }
        string factoringContractNumber { get; set; }
        string cancelComment {get; set;}
         string stornoComment {get; set;}

    }
}
