using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eInvoice.Core.Models
{
    public class SearchInvoiceResultDto
    {
        public int total { get; set; }
        public int totalSum { get; set; }
        public List<Invoice> invoices { get; set; }
        public string requestId { get; set; }
    }

    public class Invoice
    {
        public int receiverId { get; set; }
        public bool receiverIsDeleted { get; set; }
        public int channel { get; set; }
        public string channelAdress { get; set; }
        public string serviceProvider { get; set; }
        public string errorCode { get; set; }
        public string status { get; set; }
        public string cirStatus { get; set; }
        public string cirInvoiceId { get; set; }
        public string invoiceType { get; set; }
        public string serviceId { get; set; }
        public int invoiceId { get; set; }
        public int senderId { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }
        public string invoiceNumber { get; set; }
        public DateTime accountingDateUtc { get; set; }
        public DateTime paymentDateUtc { get; set; }
        public DateTime invoiceDateUtc { get; set; }
        public DateTime invoiceSentDateUtc { get; set; }
        public string referenceNumber { get; set; }
        public int fineRatePerDay { get; set; }
        public string description { get; set; }
        public string note { get; set; }
        public string orderNumber { get; set; }
        public string currency { get; set; }
        public int discountPercentage { get; set; }
        public int discountAmount { get; set; }
        public int sumWithoutVat { get; set; }
        public int vatRate { get; set; }
        public int vatSum { get; set; }
        public int sumWithVat { get; set; }
        public DateTime createdUtc { get; set; }
        public DateTime lastModifiedUtc { get; set; }
        public int version { get; set; }
        public string modelNumber { get; set; }
    }

}
