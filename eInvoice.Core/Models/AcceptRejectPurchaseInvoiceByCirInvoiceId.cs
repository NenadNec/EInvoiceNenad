using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eInvoice.Core.Models
{
    public class AcceptRejectPurchaseInvoiceByCirInvoiceId
    {
        public string cirInvoiceId { get; set; }
        public bool accepted { get; set; }
        public string comment { get; set; }
    }
}
