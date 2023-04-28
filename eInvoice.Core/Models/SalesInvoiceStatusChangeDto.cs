using System;
using System.Collections.Generic;
using System.Text;
using static eInvoice.Core.Models.Enums;

namespace eInvoice.Core.Models
{
    public class SalesInvoiceStatusChangeDto
    {
        public int eventId { get; set; }
        public string date { get; set; }
        public SalesInvoiceStatus newInvoiceStatus { get; set; }
        public int salesInvoiceId { get; set; }
        public string comment { get; set; }
        public string cirInvoiceId { get; set; }
        public string subscriptionKey { get; set; }

    }
}
