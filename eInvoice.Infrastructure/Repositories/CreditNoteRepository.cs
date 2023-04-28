using eInvoice.Core.Models;
using eInvoice.Core.Models.ERP;
using eInvoice.Infrastructure.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace eInvoice.Infrastructure.Repositories
{
    class CreditNoteRepository : BaseRepository, ICreditNoteRepository
    {
        public string FillCreditNote(Request_CreditNote2 request)
        {
            XmlDocument xml_doc = new XmlDocument();

            string crn_ns = "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2";
            string cbc_ns = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
            string cac_ns = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
            string xsi_ns = "http://www.w3.org/2001/XMLSchema-instance";
            string xsd_ns = "http://www.w3.org/2001/XMLSchema";

            XmlNamespaceManager namespaces = new XmlNamespaceManager(xml_doc.NameTable);
            namespaces.AddNamespace("crn", crn_ns);
            namespaces.AddNamespace("cbc", cbc_ns);
            namespaces.AddNamespace("cac", cac_ns);
            namespaces.AddNamespace("xsi", xsi_ns);
            namespaces.AddNamespace("xsd", xsd_ns);
            xml_doc.Load(ubl_storage + "creditnote_file.xml");

            XmlNode RootNode = xml_doc.SelectSingleNode("//crn:CreditNote", namespaces);

            XmlNode BrojFakture = xml_doc.SelectSingleNode("//crn:CreditNote/cbc:ID", namespaces);
            BrojFakture.InnerText = request.BrojFakture;

            XmlNode DatumDatumIzdavanjaFakture = xml_doc.SelectSingleNode("//crn:CreditNote/cbc:IssueDate", namespaces);
            DatumDatumIzdavanjaFakture.InnerText = request.DatumIzdavanjaRacuna.ToString("yyyy-MM-dd");

            XmlNode SifraVrsteFakture = xml_doc.SelectSingleNode("//crn:CreditNote/cbc:CreditNoteTypeCode", namespaces);
            SifraVrsteFakture.InnerText = request.SifraVrsteFakture;

            XmlNode NapomeneNaFakturi = xml_doc.SelectSingleNode("//crn:CreditNote/cbc:Note", namespaces);
            NapomeneNaFakturi.InnerText = request.NapomeneNaFakturi;

            XmlNode SifraValuteFakture = xml_doc.SelectSingleNode("//crn:CreditNote/cbc:DocumentCurrencyCode", namespaces);
            SifraValuteFakture.InnerText = request.SifraValuteFakture;

            XmlNode InterniBroj = xml_doc.SelectSingleNode("//crn:CreditNote/cbc:BuyerReference", namespaces);
            InterniBroj.InnerText = request.InterniBroj;

            XmlNode SifraDatumaPoreskeObaveze = xml_doc.SelectSingleNode("//crn:CreditNote/cac:InvoicePeriod/cbc:DescriptionCode", namespaces);
            SifraDatumaPoreskeObaveze.InnerText = request.SifraDatumaPoreskeObaveze;

            XmlNode BrojNarudzbenice = xml_doc.SelectSingleNode("//crn:CreditNote/cac:OrderReference/cbc:ID", namespaces);
            BrojNarudzbenice.InnerText = request.BrojNarudzbenice;

            XmlNode BrojOtpremnice = xml_doc.SelectSingleNode("//crn:CreditNote/cac:DespatchDocumentReference/cbc:ID", namespaces);
            BrojOtpremnice.InnerText = request.BrojOtpremnice;


            foreach (var item in request.ReferencaNaPrethodnuFakturu)
            {

                XmlNode RootElement = xml_doc.SelectSingleNode("//crn:CreditNote", namespaces);
                XmlNode RefContract = xml_doc.SelectSingleNode("//crn:CreditNote/cac:OrderReference", namespaces);
                XmlNode BillingRefercenceElement = xml_doc.CreateElement("cac", "BillingReference", cac_ns);
                RootElement.InsertAfter(BillingRefercenceElement, RefContract);

                XmlElement InvoiceDocumentReference = xml_doc.CreateElement("cac", "InvoiceDocumentReference", cac_ns);
                BillingRefercenceElement.AppendChild(InvoiceDocumentReference);

                XmlElement InvoiceDocumentReferenceID = xml_doc.CreateElement("cbc", "ID", cbc_ns);
                InvoiceDocumentReferenceID.InnerText = item.ReferencaNaPrethodnuFakturu.ToString();
                InvoiceDocumentReference.AppendChild(InvoiceDocumentReferenceID);

                XmlElement InvoiceDocumentReferenceIssueDate = xml_doc.CreateElement("cbc", "IssueDate", cbc_ns);
                InvoiceDocumentReferenceIssueDate.InnerText = item.DatumIzdavanjaPredhodnogRacuna.ToString("yyyy-MM-dd");
                InvoiceDocumentReference.AppendChild(InvoiceDocumentReferenceIssueDate);

            }



            XmlNode BrojOkvirnogSporazuma = xml_doc.SelectSingleNode("//crn:CreditNote/cac:OriginatorDocumentReference/cbc:ID", namespaces);
            BrojOkvirnogSporazuma.InnerText = request.BrojOkvirnogSporazuma;

            XmlNode BrojUgovora = xml_doc.SelectSingleNode("//crn:CreditNote/cac:ContractDocumentReference/cbc:ID", namespaces);
            BrojUgovora.InnerText = request.BrojUgovora;

            if (request.PrilozeniPDF.Count <= 3)
            {

                foreach (var line1 in request.PrilozeniPDF.Select((value1, n) => new { n, value1 }))
                {


                    XmlNode RootElement = xml_doc.SelectSingleNode("//crn:CreditNote", namespaces);
                    XmlNode RefContract = xml_doc.SelectSingleNode("//crn:CreditNote/cac:ContractDocumentReference", namespaces);
                    XmlNode AdditionalDocumentReference = xml_doc.CreateElement("cac", "AdditionalDocumentReference", cac_ns);
                    RootElement.InsertAfter(AdditionalDocumentReference, RefContract);

                    XmlElement IdentifikatorObjektaFakturisanja = xml_doc.CreateElement("cbc", "ID", cbc_ns);
                    IdentifikatorObjektaFakturisanja.InnerText = line1.value1.IdentifikatorObjektaFakturisanja.ToString();
                    AdditionalDocumentReference.AppendChild(IdentifikatorObjektaFakturisanja);

                    XmlElement Attachment = xml_doc.CreateElement("cac", "Attachment", cac_ns);
                    AdditionalDocumentReference.AppendChild(Attachment);

                    XmlElement TaxableAmountElement1 = xml_doc.CreateElement("cbc", "EmbeddedDocumentBinaryObject", cbc_ns);
                    TaxableAmountElement1.InnerText = line1.value1.PrilozeniPDF.ToString();
                    TaxableAmountElement1.SetAttribute("mimeCode", "application/pdf");
                    Attachment.AppendChild(TaxableAmountElement1);


                }
            }

            #region Prodavac

            XmlNode PIBProdavcaEndpointID = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID", namespaces);
            PIBProdavcaEndpointID.InnerText = request.PIBProdavca;


            XmlNode IdentifikatorProdavca = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification", namespaces);
            if (request.IdentifikatorProdavca == null)
            {
                XmlNode AccountingSupplierParty_Party = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingSupplierParty/cac:Party", namespaces);
                AccountingSupplierParty_Party.RemoveChild(IdentifikatorProdavca);
            }
            else
            {
                XmlNode AccountingSupplierParty_Party = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification/cbc:ID", namespaces);
                IdentifikatorProdavca.InnerText = "JBKJS:" + request.IdentifikatorProdavca;
            }

            XmlNode TrgovackiNazivProdavca = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name", namespaces);
            TrgovackiNazivProdavca.InnerText = request.TrgovackiNazivProdavca;

            XmlNode MestoProdavca = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:CityName", namespaces);
            MestoProdavca.InnerText = request.MestoProdavca;

            XmlNode SifraDrzaveProdavca = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cac:Country/cbc:IdentificationCode", namespaces);
            SifraDrzaveProdavca.InnerText = request.SifraDrzaveProdavca;

            /*XmlNode NazivDrzaveProdavca = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cac:Country/cbc:Name", namespaces);
            NazivDrzaveProdavca.InnerText = request.NazivDrzaveProdavca; //Ovo nema u PDF-u, ali ima node u XML*/

            XmlNode PIBProdavca = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme/cbc:CompanyID ", namespaces);
            PIBProdavca.InnerText = "RS" + request.PIBProdavca;

            XmlNode ProdavacEvidencijaPDV = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme/cac:TaxScheme/cbc:ID", namespaces);
            ProdavacEvidencijaPDV.InnerText = request.ProdavacEvidencijaPDV == true ? "VAT" : "NO-VAT";

            XmlNode PoslovnoImeProdavca = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:RegistrationName", namespaces);
            PoslovnoImeProdavca.InnerText = request.PoslovnoImeProdavca;

            XmlNode MaticniBrojProdavca = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID", namespaces);
            MaticniBrojProdavca.InnerText = request.MaticniBrojProdavca;

            XmlNode EMailAdresaProdavca = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:Contact/cbc:ElectronicMail", namespaces);
            EMailAdresaProdavca.InnerText = request.EMailAdresaProdavca;


            #endregion

            #region Kupac

            XmlNode PIBKupcaEndpointID = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID", namespaces);
            PIBKupcaEndpointID.InnerText = request.PIBKupca;

            XmlNode IdentifikatorKupca = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PartyIdentification", namespaces);
            if (request.IdentifikatorKupca == null)
            {
                XmlNode AccountingSupplierParty_Party = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingCustomerParty/cac:Party", namespaces);
                AccountingSupplierParty_Party.RemoveChild(IdentifikatorKupca);
            }
            else
            {
                IdentifikatorKupca = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PartyIdentification/cbc:ID", namespaces);
                IdentifikatorKupca.InnerText = "JBKJS:" + request.IdentifikatorKupca;
            }

            XmlNode TrgovackiNazivKupca = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name", namespaces);
            TrgovackiNazivKupca.InnerText = request.TrgovackiNazivKupca;

            XmlNode MestoKupca = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:CityName", namespaces);
            MestoKupca.InnerText = request.MestoKupca;

            XmlNode PostanskiBrojKupca = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:PostalZone", namespaces);
            PostanskiBrojKupca.InnerText = request.PostanskiBrojKupca;

            XmlNode SifraDrzaveKupca = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cac:Country/cbc:IdentificationCode", namespaces);
            SifraDrzaveKupca.InnerText = request.SifraDrzaveKupca;

            XmlNode PIBKupca = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PartyTaxScheme/cbc:CompanyID", namespaces);
            PIBKupca.InnerText = "RS" + request.PIBKupca;

            XmlNode KupacEvidencijaPDV = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PartyTaxScheme/cac:TaxScheme/cbc:ID", namespaces);
            KupacEvidencijaPDV.InnerText = request.KupacEvidencijaPDV == true ? "VAT" : "NO-VAT";

            XmlNode PoslovnoImeKupca = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity/cbc:RegistrationName", namespaces);
            PoslovnoImeKupca.InnerText = request.PoslovnoImeKupca;

            XmlNode MaticniBrojKupca_JBKJS = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID", namespaces);
            MaticniBrojKupca_JBKJS.InnerText = request.MaticniBrojKupca;

            XmlNode EMailAdresaKupca = xml_doc.SelectSingleNode("//crn:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:Contact/cbc:ElectronicMail", namespaces);
            EMailAdresaKupca.InnerText = request.EMailAdresaKupca;

            #endregion

            #region Dostava

            /* XmlNode StvarniDatumIsporuke = xml_doc.SelectSingleNode("//CreditNote/cac:Delivery/cbc:ActualDeliveryDate", namespaces);
             StvarniDatumIsporuke.InnerText = request.StvarniDatumIsporuke.ToString("yyyy-MM-dd");*/

            #endregion

            #region Porez-TaxTotal

            XmlNode SifraNacinaPlacanja = xml_doc.SelectSingleNode("//crn:CreditNote/cac:PaymentMeans/cbc:PaymentMeansCode", namespaces);
            SifraNacinaPlacanja.InnerText = request.SifraNacinaPlacanja.ToString();

            XmlNode PozivNaBroj = xml_doc.SelectSingleNode("//crn:CreditNote/cac:PaymentMeans/cbc:PaymentID", namespaces);
            PozivNaBroj.InnerText = request.PozivNaBroj.ToString();

            XmlNode TekuciRacun = xml_doc.SelectSingleNode("//crn:CreditNote/cac:PaymentMeans/cac:PayeeFinancialAccount/cbc:ID", namespaces);
            TekuciRacun.InnerText = request.TekuciRacun.ToString();

            XmlNode UkupanIznosPDV = xml_doc.SelectSingleNode("//crn:CreditNote/cac:TaxTotal/cbc:TaxAmount", namespaces);
            UkupanIznosPDV.InnerText = request.UkupanIznosPDV.ToString();

            XmlNode TaxTotal = xml_doc.SelectSingleNode("//crn:CreditNote/cac:TaxTotal", namespaces);

            List<CreditNote_TaxSubtotal> CreditNote_ListSubtotal = request.PoreskiMeduzbir.GroupBy(x => new { x.StopaPDVZaKategoriju, x.SifraKategorijePDV, x.SifraRazlogaZaoslobodjenjeOdPDV, x.RazlogZaOslobodjenjeOdPDV }).Select(y => new CreditNote_TaxSubtotal
            {
                IznosPDVOsnovice = y.Sum(a => a.IznosPDVOsnovice),
                IznosPorezaZaKategorijuPDV = y.Sum(a => a.IznosPorezaZaKategorijuPDV),
                StopaPDVZaKategoriju = y.Key.StopaPDVZaKategoriju,
                SifraKategorijePDV = y.Key.SifraKategorijePDV,
                SifraRazlogaZaoslobodjenjeOdPDV = y.Key.SifraRazlogaZaoslobodjenjeOdPDV,
                RazlogZaOslobodjenjeOdPDV = y.Key.RazlogZaOslobodjenjeOdPDV



            }).ToList();

            foreach (var item in CreditNote_ListSubtotal)
            {

                XmlElement TaxSubtotalElement = xml_doc.CreateElement("cac", "TaxSubtotal", cac_ns);
                TaxTotal.AppendChild(TaxSubtotalElement);

                XmlElement TaxableAmountElement = xml_doc.CreateElement("cbc", "TaxableAmount", cbc_ns);
                TaxableAmountElement.InnerText = item.IznosPDVOsnovice.ToString();
                TaxableAmountElement.SetAttribute("currencyID", request.SifraValuteFakture);
                TaxSubtotalElement.AppendChild(TaxableAmountElement);


                XmlElement TaxAmountElement = xml_doc.CreateElement("cbc", "TaxAmount", cbc_ns);
                TaxAmountElement.InnerText = item.IznosPorezaZaKategorijuPDV.ToString();
                TaxAmountElement.SetAttribute("currencyID", request.SifraValuteFakture);
                TaxSubtotalElement.AppendChild(TaxAmountElement);


                XmlElement TaxCategoryElement = xml_doc.CreateElement("cac", "TaxCategory", cac_ns);
                TaxSubtotalElement.AppendChild(TaxCategoryElement);


                XmlElement TaxCategoryIDElement = xml_doc.CreateElement("cbc", "ID", cbc_ns);
                TaxCategoryIDElement.InnerText = item.SifraKategorijePDV.ToString();
                TaxCategoryElement.AppendChild(TaxCategoryIDElement);


                XmlElement PercentElement = xml_doc.CreateElement("cbc", "Percent", cbc_ns);
                PercentElement.InnerText = item.StopaPDVZaKategoriju.ToString();
                TaxCategoryElement.AppendChild(PercentElement);

                if (item.SifraKategorijePDV != "S")
                {

                    XmlElement TaxCategoryTaxExemptionReasonCode = xml_doc.CreateElement("cbc", "TaxExemptionReasonCode", cbc_ns);
                    TaxCategoryTaxExemptionReasonCode.InnerText = item.SifraRazlogaZaoslobodjenjeOdPDV.ToString();
                    TaxCategoryElement.AppendChild(TaxCategoryTaxExemptionReasonCode);

                    XmlElement TaxCategoryTaxExemptionReason = xml_doc.CreateElement("cbc", "TaxExemptionReason", cbc_ns);
                    TaxCategoryTaxExemptionReason.InnerText = item.RazlogZaOslobodjenjeOdPDV.ToString();
                    TaxCategoryElement.AppendChild(TaxCategoryTaxExemptionReason);

                }

                XmlElement TaxSchemeElement = xml_doc.CreateElement("cac", "TaxScheme", cac_ns);
                TaxCategoryElement.AppendChild(TaxSchemeElement);

                XmlElement TaxSchemeIDElement = xml_doc.CreateElement("cbc", "ID", cbc_ns);
                TaxSchemeIDElement.InnerText = "VAT";
                TaxSchemeElement.AppendChild(TaxSchemeIDElement);

            }
            #endregion

            #region LegalMonetaryTotal
            XmlNode ZbirNetoIznosaIzStavkiRacuna = xml_doc.SelectSingleNode("//crn:CreditNote/cac:LegalMonetaryTotal/cbc:LineExtensionAmount", namespaces);
            ZbirNetoIznosaIzStavkiRacuna.InnerText = request.Sumarno.ZbirNetoIznosaIzStavkiRacuna.ToString();

            XmlNode UkupanIznosSaPDVom = xml_doc.SelectSingleNode("//crn:CreditNote/cac:LegalMonetaryTotal/cbc:TaxInclusiveAmount", namespaces);
            UkupanIznosSaPDVom.InnerText = request.Sumarno.UkupanIznosSaPDV.ToString();

            XmlNode UkupanIznosBezPDVom = xml_doc.SelectSingleNode("//crn:CreditNote/cac:LegalMonetaryTotal/cbc:TaxExclusiveAmount", namespaces);
            UkupanIznosBezPDVom.InnerText = request.Sumarno.UkupanIznosBezPDV.ToString();

            XmlNode ZbirPopustaNaDoumenta = xml_doc.SelectSingleNode("//crn:CreditNote/cac:LegalMonetaryTotal/cbc:AllowanceTotalAmount", namespaces);
            ZbirPopustaNaDoumenta.InnerText = request.Sumarno.ZbirPopustaNaNivouDokumenta.ToString();

            XmlNode PlacenIznos = xml_doc.SelectSingleNode("//crn:CreditNote/cac:LegalMonetaryTotal/cbc:PrepaidAmount", namespaces);
            PlacenIznos.InnerText = request.Sumarno.PlacenIznos.ToString();

            XmlNode IznosZaPlacanje = xml_doc.SelectSingleNode("//crn:CreditNote/cac:LegalMonetaryTotal/cbc:PayableAmount", namespaces);
            IznosZaPlacanje.InnerText = request.Sumarno.IznosZaPlacanje.ToString();

            #endregion

            #region CreditNoteLine-Stavke fakture


            foreach (var line in request.StavkeFakture.Select((value, i) => new { i, value }))
            {
                XmlElement InvoiceLineElement = xml_doc.CreateElement("cac", "CreditNoteLine", cac_ns);
                RootNode.AppendChild(InvoiceLineElement);

                XmlElement InvoiceLineIDElement = xml_doc.CreateElement("cbc", "ID", cbc_ns);
                InvoiceLineIDElement.InnerText = (line.i + 1).ToString();
                InvoiceLineElement.AppendChild(InvoiceLineIDElement);

                XmlElement InvoicedQuantityElement = xml_doc.CreateElement("cbc", "CreditedQuantity", cbc_ns);
                InvoicedQuantityElement.InnerText = line.value.FakturisanaKolicina.ToString();
                InvoicedQuantityElement.SetAttribute("unitCode", line.value.JedinicaMere);
                InvoiceLineElement.AppendChild(InvoicedQuantityElement);


                XmlElement LineExtensionAmountElement = xml_doc.CreateElement("cbc", "LineExtensionAmount", cbc_ns);
                LineExtensionAmountElement.InnerText = line.value.NetoIznosStavke.ToString();
                LineExtensionAmountElement.SetAttribute("currencyID", request.SifraValuteFakture);
                InvoiceLineElement.AppendChild(LineExtensionAmountElement);

                if (line.value.ProcenatTroskaNaStavci > 0)
                {

                    XmlElement AllowanceCharge = xml_doc.CreateElement("cac", "AllowanceCharge", cac_ns);
                    InvoiceLineElement.AppendChild(AllowanceCharge);

                    XmlElement ChargeIndicator = xml_doc.CreateElement("cbc", "ChargeIndicator", cbc_ns);
                    ChargeIndicator.InnerText = line.value.ProcenatTroskaNaStavci > 0 ? "false" : "true";
                    AllowanceCharge.AppendChild(ChargeIndicator);

                    XmlElement MultiplierFactorNumeric = xml_doc.CreateElement("cbc", "MultiplierFactorNumeric", cbc_ns);
                    MultiplierFactorNumeric.InnerText = line.value.ProcenatTroskaNaStavci.ToString();
                    AllowanceCharge.AppendChild(MultiplierFactorNumeric);

                    XmlElement Amount = xml_doc.CreateElement("cbc", "Amount", cbc_ns);
                    Amount.SetAttribute("currencyID", request.SifraValuteFakture);
                    Amount.InnerText = line.value.PopustNaCenuArtikla.ToString();
                    AllowanceCharge.AppendChild(Amount);

                }


                XmlElement Item = xml_doc.CreateElement("cac", "Item", cac_ns);
                InvoiceLineElement.AppendChild(Item);

                /* XmlElement Item_Description = xml_doc.CreateElement("cbc", "Description", cbc_ns);
                 Item_Description.InnerText = line.value.OpisArtikla;
                 Item.AppendChild(Item_Description);*/

                XmlElement Item_Name = xml_doc.CreateElement("cbc", "Name", cbc_ns);
                Item_Name.InnerText = line.value.NazivArtikla;
                Item.AppendChild(Item_Name);

                XmlElement SellersItemIdentification = xml_doc.CreateElement("cac", "SellersItemIdentification", cac_ns);
                Item.AppendChild(SellersItemIdentification);

                XmlElement SellersItemIdentification_ID = xml_doc.CreateElement("cbc", "ID", cbc_ns);
                SellersItemIdentification_ID.InnerText = line.value.IdentifikatorArtiklaKodProdavca;
                SellersItemIdentification.AppendChild(SellersItemIdentification_ID);

                XmlElement ClassifiedTaxCategory = xml_doc.CreateElement("cac", "ClassifiedTaxCategory", cac_ns);
                Item.AppendChild(ClassifiedTaxCategory);

                XmlElement ClassifiedTaxCategory_ID = xml_doc.CreateElement("cbc", "ID", cbc_ns);
                ClassifiedTaxCategory_ID.InnerText = line.value.SifraKategorijePDVZaStavku.ToString();//Ovo se menja, treba da bude "S"
                ClassifiedTaxCategory.AppendChild(ClassifiedTaxCategory_ID);

                XmlElement ClassifiedTaxCategory_Percent = xml_doc.CreateElement("cbc", "Percent", cbc_ns);
                ClassifiedTaxCategory_Percent.InnerText = line.value.StopaPDVZaStavku.ToString();
                ClassifiedTaxCategory.AppendChild(ClassifiedTaxCategory_Percent);

                XmlElement TaxScheme = xml_doc.CreateElement("cac", "TaxScheme", cac_ns);
                ClassifiedTaxCategory.AppendChild(TaxScheme);

                XmlElement ClassifiedTaxCategory_TaxScheme_ID = xml_doc.CreateElement("cbc", "ID", cbc_ns);
                ClassifiedTaxCategory_TaxScheme_ID.InnerText = "VAT";
                TaxScheme.AppendChild(ClassifiedTaxCategory_TaxScheme_ID);

                XmlElement Price = xml_doc.CreateElement("cac", "Price", cac_ns);
                InvoiceLineElement.AppendChild(Price);

                XmlElement PriceAmount = xml_doc.CreateElement("cbc", "PriceAmount", cbc_ns);
                PriceAmount.SetAttribute("currencyID", request.SifraValuteFakture);
                PriceAmount.InnerText = line.value.NetoCenaArtikla.ToString();
                Price.AppendChild(PriceAmount);



            }


            #endregion
            return xml_doc.OuterXml;
        }



        public async Task<CompanyAccountOnEfAkturaDto> CheckIfCompanyRegisteredOnEfaktura(CompanyAccountIdentificationDto bodyCheck)
        {

            CompanyAccountOnEfAkturaDto result = new CompanyAccountOnEfAkturaDto();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/Company/CheckIfCompanyRegisteredOnEfaktura");
            request.Headers.Add("Accept", "application/json");
            request.Content = new StringContent(JsonConvert.SerializeObject(bodyCheck), Encoding.UTF8, "application/json");


            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string response_string = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<CompanyAccountOnEfAkturaDto>(response_string);
                return result;
            }
            else
            {
                string error_string = await response.Content.ReadAsStringAsync();
                Error_Model error = (Error_Model)JsonConvert.DeserializeObject(error_string, typeof(Error_Model));

                return null;
            }


        }

        #region EmailHeaderTemplateCompaniyRegistrationTrue
        public string EmailHeaderTemplateCompaniyRegistrationTrue(Request_CreditNote2 body)
        {

            string htmlBody = "";
            htmlBody += "<html><tbody>";
            htmlBody += "<tr>";
            htmlBody += "<td role='modules-container' style='padding:0px 0px 0px 0px;color:#333333;text-align:left' bgcolor='#FFFFFF' width='100%' align='left'>";
            htmlBody += "<table class='m_7842241214441174436preheader' role='module' border='0' cellpadding='0' cellspacing='0' width='100%' style='display:none!important;opacity:0;color:transparent;height:0;width:0'></table>";
            htmlBody += "<table role='module' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout:fixed'>";
            htmlBody += "<tbody><tr>";
            htmlBody += "<td style='padding:20px 30px 35px 30px;line-height:42px' height='100%' valign='top' bgcolor=''>";
            htmlBody += "<div style='text-align:center'>";
            htmlBody += "<h2 style='text-align:center'>Pozdrav, <strong><span style='font-size:20px; color:#01b9ff'>" + body.PoslovnoImeProdavca + "</span>&nbsp; </strong></h2>";
            htmlBody += "</div></td></tr></tbody></table>";
            htmlBody += "<table role='module' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout:fixed'><tbody><tr>";
            htmlBody += "<td style='padding:0px 20px 0px 20px' height='100%' valign='top' bgcolor=''>";
            htmlBody += "<h2 style='text-align:center'>Poslali ste fakturu kupcu: <span style='color:#01b9ff'>" + body.PoslovnoImeKupca;
            htmlBody += "<span style='color:#626262'>";
            htmlBody += "<span style='font-size:14px'>";
            htmlBody += "<span style='font-family:arial,helvetica,sans-serif'></span>";
            htmlBody += "</span></span></div></td></tr></tbody></table>";
            htmlBody += "<table role='module' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout:fixed'>";
            htmlBody += "<tbody><tr>";
            htmlBody += "<td style='padding:0px 20px 0px 20px' height='100%' valign='top' bgcolor=''>";
            htmlBody += "<div style='text-align:center'>";
            htmlBody += "<table border='0' cellpadding='0' cellspacing='3'>";
            htmlBody += "<tbody><tr><td>";
            htmlBody += "";
            htmlBody += " </td></tr></tbody></table></div></td></tr></tbody></table>";
            htmlBody += "<table role='module' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout:fixed'>";
            htmlBody += "<tbody><tr>";
            htmlBody += "<td height='100%' valign='top'>";
            htmlBody += "<div style='padding:10px 0px 0px 20px;text-align:center'>";
            htmlBody += "<div>&nbsp;</div>";
            htmlBody += "<p style='padding-top: 6pt;padding-left: 11pt;text-indent: 0pt;text-align: center;'><span ='mailto:office@docus.co.rs' class='a' target='_blank'>Create by DOCUS | </span><a href='http://www.docus.co.rs/' class='a' target='_blank'> </a><span>office@docus.co.rs | Telefon +381 32 310050 | </span><a href='http://www.docus.co.rs/' target='_blank'>www.docus.co.rs</a></p>";
            htmlBody += "</div></td></tr></tbody></table></td></tr></tbody></html>";

            return htmlBody;
        }
        #endregion EmailHeaderTemplateCompaniyRegistrationTrue

        #region EmailHeaderTemplateCompaniyRegistrationFalse
        public string EmailHeaderTemplateCompaniyRegistrationFalse(Request_CreditNote2 body)
        {
            string htmlBody = "";
            htmlBody += "<html><tbody>";
            htmlBody += "<tr>";
            htmlBody += "<td role='modules-container' style='padding:0px 0px 0px 0px;color:#333333;text-align:left' bgcolor='#FFFFFF' width='100%' align='left'>";
            htmlBody += "<table class='m_7842241214441174436preheader' role='module' border='0' cellpadding='0' cellspacing='0' width='100%' style='display:none!important;opacity:0;color:transparent;height:0;width:0'></table>";
            htmlBody += "<table role='module' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout:fixed'>";
            htmlBody += "<tbody><tr>";
            htmlBody += "<td style='padding:20px 30px 35px 30px;line-height:42px' height='100%' valign='top' bgcolor=''>";
            htmlBody += "<div style='text-align:center'>";
            htmlBody += "<h2 style='text-align:center'>Pozdrav, <strong><span style='font-size:20px; color:#01b9ff'>" + body.PoslovnoImeProdavca + "</span>&nbsp; </strong></h2>";
            htmlBody += "</div></td></tr></tbody></table>";
            htmlBody += "<table role='module' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout:fixed'><tbody><tr>";
            htmlBody += "<td style='padding:0px 20px 0px 20px' height='100%' valign='top' bgcolor=''>";
            htmlBody += "<h2 style='text-align:center'>Kupac: <span style='color:#01b9ff'>" + body.PoslovnoImeKupca + "</span><h2 style='text-align:center'>nema otvoren nalog na portalu E-fakture</h2";
            htmlBody += "<span style='color:#626262'>";
            htmlBody += "<span style='font-size:14px'>";
            htmlBody += "<span style='font-family:arial,helvetica,sans-serif'></span>";
            htmlBody += "</span></span></div></td></tr></tbody></table>";
            htmlBody += "<table role='module' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout:fixed'>";
            htmlBody += "<tbody><tr>";
            htmlBody += "<td style='padding:0px 20px 0px 20px' height='100%' valign='top' bgcolor=''>";
            htmlBody += "<div style='text-align:center'>";
            htmlBody += "<table border='0' cellpadding='0' cellspacing='3'>";
            htmlBody += "<tbody><tr><td>";
            htmlBody += "";
            htmlBody += " </td></tr></tbody></table></div></td></tr></tbody></table>";
            htmlBody += "<table role='module' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout:fixed'>";
            htmlBody += "<tbody><tr>";
            htmlBody += "<td height='100%' valign='top'>";
            htmlBody += "<div style='padding:10px 0px 0px 20px;text-align:center'>";
            htmlBody += "<div>&nbsp;</div>";
            htmlBody += "<p style='padding-top: 6pt;padding-left: 11pt;text-indent: 0pt;text-align: center;'><span ='mailto:office@docus.co.rs' class='a' target='_blank'>Create by DOCUS | </span><a href='http://www.docus.co.rs/' class='a' target='_blank'> </a><span>office@docus.co.rs | Telefon +381 32 310050 | </span><a href='http://www.docus.co.rs/' target='_blank'>www.docus.co.rs</a></p>";
            htmlBody += "</div></td></tr></tbody></table></td></tr></tbody></html>";

            return htmlBody;
        }
        #endregion EmailHeaderTemplateCompaniyRegistrationFalse

        public async Task SendEmail(Request_CreditNote2 body)
        {
            CompanyAccountIdentificationDto bodyCheck = new CompanyAccountIdentificationDto();
            bodyCheck.registrationNumber = body.MaticniBrojKupca;
            bodyCheck.jbkjs = body.IdentifikatorKupca;
            CompanyAccountOnEfAkturaDto CheckCompany = await CheckIfCompanyRegisteredOnEfaktura(bodyCheck);
            string HtmlHeaderTrue = EmailHeaderTemplateCompaniyRegistrationTrue(body);
            string HtmlHeaderFalse = EmailHeaderTemplateCompaniyRegistrationFalse(body);

            using (MemoryStream memoryStream = new MemoryStream())
            {

                string base64BinaryStr = body.PrilozeniPDF.Select(x => x.PrilozeniPDF).First();
                var imageBytes = Convert.FromBase64String(base64BinaryStr);

                var smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;//465
                smtpClient.EnableSsl = true;

                smtpClient.Credentials = new NetworkCredential("probatest229@gmail.com", "bzteagrltwvdtleo");//Test123Proba!
                smtpClient.UseDefaultCredentials = false;

                var fromAdress = new MailAddress("probatest229@gmail.com");
                var toAdress = new MailAddress(body.EMailAdresaProdavca);
                var message = new MailMessage(fromAdress, toAdress);

                if (CheckCompany.eFakturaRegisteredCompany == true)
                {
                    message.Body = HtmlHeaderTrue;
                    message.Subject = "OBAVEŠTENJE- Faktura: " + body.BrojFakture + " je poslata kupcu " + body.TrgovackiNazivKupca;
                }
                else
                {
                    message.Body = HtmlHeaderFalse;
                    message.Subject = "OBAVEŠTENJE- Faktura: " + body.BrojFakture + " nije poslata kupcu " + body.TrgovackiNazivKupca;
                }
                if (body.PrilozeniPDF.Select(x => x.PrilozeniPDF).First() != "")
                {
                    message.Attachments.Add(new Attachment(new MemoryStream(imageBytes), body.BrojFakture + ".pdf"));
                }
                message.IsBodyHtml = true;
                message.Priority = MailPriority.High;

                smtpClient.Send(message);
            }

        }



    }


}
