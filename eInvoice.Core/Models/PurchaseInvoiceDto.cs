using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eInvoice.Core.Models
{
    public class PurchaseInvoiceDto
    {
        public int invoiceId { get; set; }
        public int senderApplicationId { get; set; }
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public int receiverUserAccountId { get; set; }
        public string status { get; set; }
        public string invoiceNumber { get; set; }
        public string senderReceiverContractNumber { get; set; }
        public DateTime accountingDateUtc { get; set; }
        public DateTime invoiceDateUtc { get; set; }
        public DateTime paymentDateUtc { get; set; }
        public string referenceNumber { get; set; }
        public string modelNumber { get; set; }
        public int fineRatePerDay { get; set; }
        public string description { get; set; }
        public string note { get; set; }
        public string orderNumber { get; set; }
        public string currencyName { get; set; }
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
        public List<pidto_File> files { get; set; }
        public List<pidto_CustomField> customFields { get; set; }
        public string invoiceMessage { get; set; }
        public string acceptRejectMessage { get; set; }
        public string cirInvoiceId { get; set; }
        public string cirAmountChangeId { get; set; }
        public string cirStatus { get; set; }
        public pidto_CirHistory cirHistory { get; set; }
        public pidto_CirAssignationHistory cirAssignationHistory { get; set; }
        public int cirSettledAmount { get; set; }
        public bool isCreditInvoice { get; set; }
        public bool isDebitNote { get; set; }
        public bool isStorno { get; set; }
        public int stornoCancelledInvoiceId { get; set; }
        public string stornoCancelledInvoiceNumber { get; set; }
        public int stornoInvoiceId { get; set; }
        public string stornoInvoiceNumber { get; set; }
        public string stornoInvoiceMessage { get; set; }
        public string cancelInvoiceMessage { get; set; }
        public pidto_SourceInvoice sourceInvoice { get; set; }
        public List<pidto_CreditInvoice> creditInvoices { get; set; }
        public List<pidto_DebitNote> debitNotes { get; set; }
        public bool isPrepaymentInvoice { get; set; }
        public bool receiverCalculatesVat { get; set; }
        public bool addVatRate { get; set; }
        public int receiverCalculatedVatRate { get; set; }
        public bool isExemptFromVat { get; set; }
        public int vatExemptionReasonId { get; set; }
        public string vatExemptionReasonKey { get; set; }
        public string vatExemtionFreeFormNote { get; set; }
        public int totalToPay { get; set; }
        public string vatPointDate { get; set; }
    }
    public class pidto_File
    {
        public int id { get; set; }
        public string name { get; set; }
        public string extension { get; set; }
        public int invoiceId { get; set; }
        public bool mainPdf { get; set; }
        public bool mainXml { get; set; }
        public bool isFitekInZip { get; set; }
    }

    public class pidto_CustomField
    {
        public string customField { get; set; }
        public string value { get; set; }
    }

    public class pidto_Assignment
    {
        public string assignmentContractNr { get; set; }
        public string assignmentDebtorName { get; set; }
        public string assignmentDebtorCompanyNr { get; set; }
        public string assignmentIdfNr { get; set; }
        public string originalIdfNr { get; set; }
    }

    public class pidto_AmountChanx
    {
        public string comments { get; set; }
        public int amount { get; set; }
        public DateTime cancelDate { get; set; }
        public DateTime creationDate { get; set; }
        public string cancelComments { get; set; }
        public int changedId { get; set; }
        public int id { get; set; }
    }

    public class pidto_Cancellation
    {
        public DateTime cancelDate { get; set; }
        public int cancelledBy { get; set; }
        public string reason { get; set; }
    }

    public class pidto_Settlement
    {
        public DateTime settlementDate { get; set; }
        public int amount { get; set; }
        public string comment { get; set; }
    }

    public class pidto_CirHistory
    {
        public string comment { get; set; }
        public pidto_Assignment assignment { get; set; }
        public List<pidto_AmountChanx> amountChanges { get; set; }
        public pidto_Cancellation cancellation { get; set; }
        public List<pidto_Settlement> settlements { get; set; }
    }

    public class pidto_User
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class pidto_InvoiceChanx
    {
        public int id { get; set; }
        public string propertyName { get; set; }
        public string oldValue { get; set; }
        public string newValue { get; set; }
        public DateTime dateChanged { get; set; }
        public pidto_User user { get; set; }
        public int version { get; set; }
        public bool serviceDesk { get; set; }
    }

    public class pidto_CirAssignationHistory
    {
        public int invoiceId { get; set; }
        public List<pidto_InvoiceChanx> invoiceChanges { get; set; }
    }

    public class pidto_SourceInvoice
    {
        public string invoiceId { get; set; }
        public string cirInvoiceId { get; set; }
        public string invoiceNumber { get; set; }
        public bool sentToCir { get; set; }
        public string status { get; set; }
    }

    public class pidto_CreditInvoice
    {
        public string invoiceId { get; set; }
        public string cirInvoiceId { get; set; }
        public string invoiceNumber { get; set; }
        public bool sentToCir { get; set; }
        public string status { get; set; }
    }

    public class pidto_DebitNote
    {
        public string invoiceId { get; set; }
        public string cirInvoiceId { get; set; }
        public string invoiceNumber { get; set; }
        public bool sentToCir { get; set; }
        public string status { get; set; }
    }


}
