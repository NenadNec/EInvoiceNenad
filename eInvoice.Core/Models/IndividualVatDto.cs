using System;
using System.Collections.Generic;
using eInvoice.Core.Models.ERP;

namespace eInvoice.Core.Models
{
    public class IndividualVatDto 
    {
        public int individualVatId {get; set;}
        public int companyId {get; set;}
        public string? documentNumber {get; set;}
        public VatRecordingStatus vatRecordingStatus {get; set;}
        public string? sendDate {get; set;}
        public string? turnoverDate {get; set;}
        public string? paymentDate {get; set;}
        public UBLInvoiceDocumentType documentType {get; set;}
        public string? turnoverDescription {get; set;}
        public double turnoverAmount {get; set;}
        public double vatBaseAmount20 {get; set;}
        public double vatBaseAmount10 {get; set;}
        public double vatAmount {get; set;}
        public double totalAmount {get; set;}
        public VatDeductionRight vatDeductionRight {get; set;}
        public List<RelatedVatDocumentDto>? relatedDocuments {get; set;}
        public DocumentDirection documentDirection {get; set;}
        public string? relatedPartyIdentifier {get; set;}
        public Boolean foreignDocument {get; set;}
    }
}