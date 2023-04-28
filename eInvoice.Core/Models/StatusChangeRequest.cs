using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eInvoice.Core.Models
{
    public class StatusChangeRequest
    {
        public string requestId { get; set; }
        public string integratorId { get; set; }
        public string authorizationToken { get; set; }
        public List<scr_Payload> payload { get; set; }
    }

    public class scr_Payload
    {
        public scr_Invoice invoice { get; set; }
    }

    public class scr_Extension
    {
        public string extensionId { get; set; }
        public string informationName { get; set; }
        public string informationContent { get; set; }
    }

    public class scr_Header
    {
        public string clientInvoiceNumber { get; set; }
        public string internalInvoiceId { get; set; }
        public List<scr_Extension> extensions { get; set; }
    }

    public class scr_Status
    {
        public string previousInvoiceStatus { get; set; }
        public string newInvoiceStatus { get; set; }
        public string comment { get; set; }
    }

    public class scr_Invoice
    {
        public scr_Header header { get; set; }
        public scr_Status status { get; set; }
    }
}
