using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static eInvoice.Core.Models.Enums;

namespace eInvoice.Core.Models
{
    public class SearchPurchaseInvoiceResultDto
    {
        public int total { get; set; }
        public int totalSum { get; set; }
        public List<spi_PurchaseInvoice> invoices { get; set; }
        public string requestId { get; set; }
    }
    public class spi_CustomField
    {
        public string customField { get; set; }
        public string value { get; set; }
    }

    public class spi_PurchaseInvoice
    {
        public int invoiceId { get; set; }
        public int senderApplicationId { get; set; }
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public int receiverUserAccountId { get; set; }
        public PurchaseInvoiceStatus status { get; set; }
        public string invoiceNumber { get; set; }
        public DateTime accountingDateUtc { get; set; }
        public DateTime invoiceDateUtc { get; set; }
        public DateTime invoiceSentDateUtc { get; set; }
        public DateTime paymentDateUtc { get; set; }
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
        public string serviceId { get; set; }
        public string invoiceFilePath { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }
        public int duplicate { get; set; }
        public bool isDeleted { get; set; }
        public List<spi_CustomField> customFields { get; set; }
        public string invoiceMessage { get; set; }
        public string acceptRejectMessage { get; set; }
        public string cirInvoiceId { get; set; }
        public CirInvoiceStatus cirStatus { get; set; }
        public InvoiceTypes invoiceType { get; set; }
    }
}
