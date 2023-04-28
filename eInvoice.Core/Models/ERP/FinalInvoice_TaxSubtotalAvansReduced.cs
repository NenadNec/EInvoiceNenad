namespace eInvoice.Core.Models.ERP
{
    public class FinaInvoice_TaxSubtotalAvansReduced
    {
      
        public decimal IznosPDVOsnoviceAvansReduced { get; set; } //Invoice/cac:TaxTotal/cac:TaxSubtotal/cbc:TaxableAmount
        public decimal IznosPorezaZaKategorijuPDVAvansReduced { get; set; } //Invoice/cac:TaxTotal/cac:TaxSubtotal/cbc:TaxAmount
        public string SifraKategorijePDVAvansReduced { get; set; } //Invoice/TaxTotal/cac:TaxSubtotal/cac:TaxCategory/cbc:ID
        public decimal StopaPDVZaKategorijuAvansReduced { get; set; } //Invoice/cac:TaxTotal/cac:TaxSubtotal/cac:TaxCategory/cbc:Percent
       // public string SifraRazlogaZaoslobodjenjeOdPDVAvansReduced { get; set; }
       // public string RazlogZaOslobodjenjeOdPDVAvansReduced { get; set; }

    }
}