susing System;
using System.Collections.Generic;
using System.Text;
using static eInvoice.Core.Models.Enums;

namespace eInvoice.Core.Models
{

    public class AcceptRejectPurchaseInvoiceResponse
    {
        public ar_PurchaseInvoice invoice { get; set; }
        public bool success { get; set; }
    }
    public class ar_PurchaseInvoice
    {
        public string invoiceNumber { get; set; }
        public PurchaseInvoiceStatus status { get; set; }
    }
}
