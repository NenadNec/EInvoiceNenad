using System;
using System.Collections.Generic;
using System.Text;

namespace eInvoice.Core.Models
{
    public class CancelInvoiceMessageDto
    {
        public int invoiceId { get; set; } //integer($int64)
        public string cancelComments { get; set; } // nullable: true
    }
}
