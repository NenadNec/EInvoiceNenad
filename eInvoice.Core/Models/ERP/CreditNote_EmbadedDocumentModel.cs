using System;
using System.Collections.Generic;

namespace eInvoice.Core.Models.ERP
{
    public class CreditNote_EmbadedDocumentModel : AuditableModel
    {
        public string PrilozeniPDF {get; set;}

        public string IdentifikatorObjektaFakturisanja{get; set;}
    }
}