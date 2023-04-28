namespace eInvoice.Core.Models.ERP
{
    public class FinaInvoice_TaxSubtotal
    {
        public decimal IznosPDVOsnovice { get; set; } //Invoice/cac:TaxTotal/cac:TaxSubtotal/cbc:TaxableAmount
        public decimal IznosPorezaZaKategorijuPDV { get; set; } //Invoice/cac:TaxTotal/cac:TaxSubtotal/cbc:TaxAmount
        public string SifraKategorijePDV { get; set; } //Invoice/TaxTotal/cac:TaxSubtotal/cac:TaxCategory/cbc:ID
        public decimal StopaPDVZaKategoriju { get; set; } //Invoice/cac:TaxTotal/cac:TaxSubtotal/cac:TaxCategory/cbc:Percent
        public string SifraRazlogaZaoslobodjenjeOdPDV { get; set; }
        public string RazlogZaOslobodjenjeOdPDV { get; set; }

    }
}