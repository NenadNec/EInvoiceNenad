using System;
using System.Collections.Generic;
using eInvoice.Core.Models.ERP;

namespace eInvoice.Core.Models
{
    public enum UBLInvoiceDocumentType
    {
        ProformaInvoice, Invoice, CreditNote,
        DebitNote, PrepaymentInvoice
    }
}