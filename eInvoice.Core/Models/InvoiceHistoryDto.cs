using System;
using System.Collections.Generic;
using System.Text;

namespace eInvoice.Core.Models
{
    public class InvoiceHistoryDto
    {
        public int invoiceId { get; set; }
        public List<ihd_InvoiceChanx> invoiceChanges { get; set; }
    }


    public class ihd_User
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class ihd_InvoiceChanx
    {
        public int id { get; set; }
        public string propertyName { get; set; }
        public string oldValue { get; set; }
        public string newValue { get; set; }
        public DateTime dateChanged { get; set; }
        public ihd_User user { get; set; }
        public int version { get; set; }
        public bool serviceDesk { get; set; }
    }

}
