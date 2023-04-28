using System.Collections.Generic;

namespace eInvoice.Core.Models.ERP
{
    public class FinaInvoice_TaxSubtotalAvans
    {
        public decimal UkupanIznosPDVKodAvansneFakture { get; set; }
        public decimal IznosPDVOsnoviceAvans { get; set; } //Invoice/cac:TaxTotal/cac:TaxSubtotal/cbc:TaxableAmount
        public decimal IznosPorezaZaKategorijuPDVAvans { get; set; } //Invoice/cac:TaxTotal/cac:TaxSubtotal/cbc:TaxAmount
        public string SifraKategorijePDVAvans { get; set; } //Invoice/TaxTotal/cac:TaxSubtotal/cac:TaxCategory/cbc:ID
        public decimal StopaPDVZaKategorijuAvans { get; set; } //Invoice/cac:TaxTotal/cac:TaxSubtotal/cac:TaxCategory/cbc:Percent
        /*public string SifraRazlogaZaoslobodjenjeOdPDVAvans { get; set; }
        public string RazlogZaOslobodjenjeOdPDVAvans { get; set; }*/
        public FinaInvoice_ReferencaFakture ReferencaNaPrethodnuFakturu { get; set; }

    }
}