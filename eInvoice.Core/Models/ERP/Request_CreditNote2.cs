using System;
using System.Collections.Generic;


namespace eInvoice.Core.Models.ERP
{
    public class Request_CreditNote2 : AuditableModel
    {
        public string BrojFakture { get; set; }//Invoice/cbc:ID  obavezno polje
        public DateTime DatumIzdavanjaRacuna { get; set; }//Invoice/cbc:IssueDate format YYYY-MM-DD
                                                          // public DateTime DatumDospecaPlacanja { get; set; }//Invoice/cbc:DueDate format YYYY-MM-DD
        public string SifraVrsteFakture { get; set; }//Invoice/cbc:InvoiceTypeCode
        public string NapomeneNaFakturi { get; set; } //Invoice/cbc:Note
        public string SifraValuteFakture { get; set; }//Invoice/cbc:DocumentCurrencyCode RSD, USD, CHF, EUR ...
        public string InterniBroj { get; set; }
        public string SifraDatumaPoreskeObaveze { get; set; }//Invoice/cac:InvoicePeriod/cbc:DescriptionCode

        //public string ReferencaNaPredhodnuFakturu { get; set; }// /CreditNote/cac:BillingReference/cac:InvoiceDocumentReference/cbc: ID -Код авансног плаћања коначни рачун наводи референце свих авансних рачуна, а код коригованог рачуна се наводи референца рачуна који је коригован
        // public DateTime DatumIzdavanjaPredhodnogRacuna { get; set; }

        public List<CreditNote_ReferencaFakture> ReferencaNaPrethodnuFakturu { get; set; }
        public string BrojNarudzbenice { get; set; }
        public string BrojOtpremnice { get; set; }
        public string BrojOkvirnogSporazuma { get; set; }
        public string BrojUgovora { get; set; }///Invoice/cac:ContractDocumentReference/cbc:ID

        public List<CreditNote_EmbadedDocumentModel> PrilozeniPDF { get; set; }

        #region  podaci o prodavcu
        public string PIBProdavca { get; set; }
        //Invoice/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID moze da bude i JMBG
        //Invoice/cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme/cbc:CompanyID dodajem prefiks RS kada se korisi kao PIB
        public string IdentifikatorProdavca { get; set; } //Invoice/cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification/cbc:ID  fiksira se "JBKJS:" ukoliko je u pitanju budzetski korisnik polje je obavezno, u suprotnom ovaj atribut se izbacuje iz XML fajla
        public string TrgovackiNazivProdavca { get; set; } //Invoice/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name
        public string MestoProdavca { get; set; } //Invoice/cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:CityName
        public string SifraDrzaveProdavca { get; set; } //Invoice/cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cac:Country/cbc:IdentificationCode 
                                                        //  public string NazivDrzaveProdavca { get; set; } //Invoice/cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cac:Country/cbc:Name 
        public bool ProdavacEvidencijaPDV { get; set; } //Invoice/cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme/cac:TaxScheme/cbc:ID ==true?"VAT":"NO-VAT"
        public string PoslovnoImeProdavca { get; set; } //Invoice/cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:RegistrationName
        public string MaticniBrojProdavca { get; set; } //Invoice/cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID
        public string EMailAdresaProdavca { get; set; }

        #endregion


        #region podaci o kupcu
        public string PIBKupca { get; set; }
        //Invoice/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID moze da bude i JMBG
        //Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyTaxScheme/cbc:CompanyID dodajem prefiks RS kada se korisi kao PIB
        public string IdentifikatorKupca { get; set; } //Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyIdentification/cbc:ID  fiksira se "JBKJS:" ukoliko je u pitanju budzetski korisnik polje je obavezno, u suprotnom ovaj atribut se izbacuje iz XML fajla
        public string TrgovackiNazivKupca { get; set; } //Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name
        public string UlicaKupca { get; set; } //Invoice/cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:StreetName
        public string MestoKupca { get; set; } //Invoice/cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:CityName
        public string PostanskiBrojKupca { get; set; } //Invoice/cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:PostalZone
        public string SifraDrzaveKupca { get; set; } //Invoice/cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cac:Country/cbc:IdentificationCode 
                                                     // public string NazivDrzaveKupca { get; set; } //Invoice/cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cac:Country/cbc:Name 
        public bool KupacEvidencijaPDV { get; set; } //Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyTaxScheme/cac:TaxScheme/cbc:ID ==""RSVAT-STATUS"
        public string PoslovnoImeKupca { get; set; } //Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity/cbc:RegistrationName
        public string MaticniBrojKupca { get; set; } //Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID
        public string EMailAdresaKupca { get; set; }

        #endregion

        public DateTime StvarniDatumIsporuke { get; set; } //Invoice/cac:Delivery/cbc:ActualDeliveryDate

        public string SifraNacinaPlacanja { get; set; }

        public string PozivNaBroj { get; set; }

        public string TekuciRacun { get; set; }

        public decimal UkupanIznosPDV { get; set; }

        /*    ////////////////////
            public decimal IznosPDVOsnovice { get; set; } //Invoice/cac:TaxTotal/cac:TaxSubtotal/cbc:TaxableAmount
            public decimal IznosPorezaZaKategorijuPDV { get; set; } //Invoice/cac:TaxTotal/cac:TaxSubtotal/cbc:TaxAmount
            public decimal IznosPorezaZaKategorijuPDV1 { get; set; } 
            public string SifraKategorijePDV { get; set; } //Invoice/TaxTotal/cac:TaxSubtotal/cac:TaxCategory/cbc:ID
            public decimal StopaPDVZaKategoriju { get; set; } //Invoice/cac:TaxTotal/cac:TaxSubtotal/cac:TaxCategory/cbc:Percent*/


        public List<CreditNote_TaxSubtotal> PoreskiMeduzbir { get; set; } //Invoice/cac:TaxTotal/cbc:TaxSubtotal
        public CreditNote_LegalMonetaryTotal Sumarno { get; set; } //Invoice/cac:TaxTotal/cbc:TaxSubtotal
        public List<CreditNote_InvoiceLine> StavkeFakture { get; set; } //Invoice/cac:TaxTotal/cbc:TaxSubtotal
    }
}