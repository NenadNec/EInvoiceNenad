namespace eInvoice.Core.Models
{
    public class StornoInvoiceMessageDto
    {
        public int invoiceId { get; set; }
        public string stornoInvoiceNumber { get; set; }
        public string stornoComment { get; set; }
    }
}