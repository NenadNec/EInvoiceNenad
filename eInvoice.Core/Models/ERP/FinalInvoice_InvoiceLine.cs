namespace eInvoice.Core.Models.ERP
{
    public class FinalInvoice_InvoiceLine
    {
        public int RedniBrojStavke { get; set; } //Invoice/cac:InvoiceLine/cbc:ID
        public int FakturisanaKolicina { get; set; } //Invoice/cac:InvoiceLine/cbc:InvoicedQuantity
        public string JedinicaMere { get; set; }
        public decimal NetoIznosStavke { get; set; } //Invoice/cac:InvoiceLine/cbc:LineExtensionAmount 
       // public decimal IznosPDV { get; set; } //Invoice/cac:InvoiceLine/cac:TaxTotal/cbc:TaxAmount
        //public decimal PoreskaOsnovica { get; set; } //Invoice/cac:InvoiceLine/cac:TaxTotal/cac:TaxSubtotal/cbc:TaxableAmount
       // public decimal UkupanIznosPDV { get; set; } //Invoice/cac:InvoiceLine/cac:TaxTotal/cac:TaxSubtotal/cbc:TaxAmount
        public string SifraKategorijePDVZaStavku { get; set; }///Invoice/cac:InvoiceLine/cac:Item/cac:ClassifiedTaxCategory/cbc:ID
        //public decimal StopaPDVZaKategoriju { get; set; } //Invoice/cac:TaxTotal/cac:TaxSubtotal/cac:TaxCategory/cbc:Percent
        //public string OpisArtikla { get; set; } //Invoice/cac:InvoiceLine/cac:Item/cbc:Description
        public string NazivArtikla { get; set; } //Invoice/cac:InvoiceLine/cac:Item/cbc:Name
        public string IdentifikatorArtiklaKodProdavca { get; set; } //Invoice/cac:InvoiceLine/cac:Item/cac:SellersItemIdentification/cbc:ID
        public decimal StopaPDVZaStavku { get; set; }//Invoice/cac:InvoiceLine/cac:Item/cac:ClassifiedTaxCategory/cbc:Percent
        public decimal NetoCenaArtikla { get; set; }//Invoice/cac:InvoiceLine/cac:Price/cbc:PriceAmount
        public decimal ProcenatTroskaNaStavci { get; set; }//Invoice/cac:InvoiceLine/cac:Price/cac:AllowanceCharge/cbc:MultiplierFactorNumeric
        public decimal PopustNaCenuArtikla { get; set; }//Invoice/cac:InvoiceLine/cac:Price/cac:AllowanceCharge/cbc:Amount


    }
}