namespace eInvoice.Core.Models.ERP
{
    public class FinalInvoice_MonetaryTotalAvans
    {
       // public decimal ZbirNetoIznosaIzStavkiRacunaAvans { get; set; } //Invoice/cac:LegalMonetaryTotal/cbc:LineExtensionAmount

        public decimal UkupanIznosBezPDVAvans { get; set; }
        public decimal UkupanIznosSaPDVAvans { get; set; } //Invoice/cac:LegalMonetaryTotal/cbc:TaxInclusiveAmount
       // public decimal ZbirPopustaNaNivouDokumentaAvans { get; set; } //Invoice/cac:LegalMonetaryTotal/cbc:AllowanceTotalAmount

       // public decimal ZbirUplataPoAvansnimRacunima { get; set; }
        public decimal IznosZaPlacanjeAvans { get; set; } //Invoice/cac:LegalMonetaryTotal/cbc:PayableAmount
       // public decimal PlacenIznosAvans {get; set;}
        //public decimal IznosPDVOsnovice { get; set; }
       // public decimal UkupanIznosPDV { get; set; } //Invoice/cac:TaxTotal/cbc:TaxAmount
    }
}