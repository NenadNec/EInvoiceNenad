using System;
using System.Collections.Generic;
using System.Text;

namespace eInvoice.Core.Models
{
    public class AcceptRejectPurchaseInvoice
    {
        public int invoiceId { get; set; }
        public bool accepted { get; set; }
        public string comment { get; set; }
    }
}
