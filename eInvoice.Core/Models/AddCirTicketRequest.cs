using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eInvoice.Core.Models
{
    public class AddCirTicketRequest
    {
        public string cirInvoiceId { get; set; }
        public string data { get; set; }
        public int amount { get; set; }
        public string userComment { get; set; }
        public string resourceType { get; set; }
        public string cirTicketCategory { get; set; }

    }
}
