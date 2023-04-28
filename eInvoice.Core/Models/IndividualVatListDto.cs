using System;
using System.Collections.Generic;
using eInvoice.Core.Models.ERP;

namespace eInvoice.Core.Models
{
    public class IndividualVatListDto
    {
        public int IndividualVatId { get; set; }
        public string? DocumentNumber { get; set; }
        public VatRecordingStatus VatRecordingStatus { get; set; }
        public DateTime? SendDate { get; set; }
        public UBLInvoiceDocumentType DocumentType { get; set; }
        public double TurnoverAmount { get; set; }
        public double VatAmount { get; set; }
        public VatDeductionRight VatDeductionRight { get; set; }
        public DocumentDirection DocumentDirection { get; set; }
        public string? RelatedPartyIdentifier { get; set; }
        public bool ForeignDocument { get; set; }
    }
}