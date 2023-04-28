namespace eInvoice.Core.Models.ERP
{
    public class FinalInvoice_MonetaryTotal
    {
        public decimal ZbirNetoIznosaIzStavkiRacuna { get; set; } //Invoice/cac:LegalMonetaryTotal/cbc:LineExtensionAmount

        public decimal UkupanIznosBezPDV { get; set; }
        public decimal UkupanIznosSaPDV { get; set; } //Invoice/cac:LegalMonetaryTotal/cbc:TaxInclusiveAmount
        public decimal ZbirPopustaNaNivouDokumenta { get; set; } //Invoice/cac:LegalMonetaryTotal/cbc:AllowanceTotalAmount

       // public decimal ZbirUplataPoAvansnimRacunima { get; set; }
        public decimal IznosZaPlacanje { get; set; } //Invoice/cac:LegalMonetaryTotal/cbc:PayableAmount
        public decimal PlacenIznos {get; set;}
        //public decimal IznosPDVOsnovice { get; set; }
       // public decimal UkupanIznosPDV { get; set; } //Invoice/cac:TaxTotal/cbc:TaxAmount
    }
}