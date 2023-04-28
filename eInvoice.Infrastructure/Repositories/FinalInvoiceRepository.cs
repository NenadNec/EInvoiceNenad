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
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace eInvoice.Infrastructure.Repositories
{
    public class FinalInvoiceRepository : BaseRepository, IFinalInvoiceRepository
    {
        public FinalInvoiceRepository()
        { }
        List<SalesInvoiceStatusChangeDto> result = new List<SalesInvoiceStatusChangeDto>();


        public string ReturnString()
        {
            var item = server_url;
            return "Test string !";
        }

        public string FillFinalInvoice(Reques_FinalInvoice request)
        {
            XmlDocument xml_doc = new XmlDocument();
            string inv_ns = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2";
            string cbc_ns = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
            string cac_ns = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
            string xsi_ns = "http://www.w3.org/2001/XMLSchema-instance";
            string xsd_ns = "http://www.w3.org/2001/XMLSchema";
            string sbt_ns = "http://mfin.gov.rs/srbdt/srbdtext";
            string cec_ns = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2";
            // string xmlns_ns = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2";
            //string sbt_ns = "http://mfin.gov.rs/srbdt/srbdtext";

            XmlNamespaceManager namespaces = new XmlNamespaceManager(xml_doc.NameTable);
            namespaces.AddNamespace("inv", inv_ns);
            namespaces.AddNamespace("cbc", cbc_ns);
            namespaces.AddNamespace("cac", cac_ns);
            namespaces.AddNamespace("xsi", xsi_ns);
            namespaces.AddNamespace("xsd", xsd_ns);
            namespaces.AddNamespace("sbt", sbt_ns);
            namespaces.AddNamespace("cec", cec_ns);
            // namespaces.AddNamespace("xmlns", xmlns_ns);

            xml_doc.Load(ubl_storage + "final_invoice.xml");

            XmlNode RootNode = xml_doc.SelectSingleNode("//inv:Invoice", namespaces);

            XmlNode TaxTotalInvoicedPrepaymentAmmount = xml_doc.SelectSingleNode("//inv:Invoice/cec:UBLExtensions/cec:UBLExtension/cec:ExtensionContent/sbt:SrbDtExt", namespaces);

            List<FinaInvoice_TaxSubtotalAvans> ListSubtotalPrepaymentAmmount = request.PoreskiMeduzbirAvans.GroupBy(x => new { x.StopaPDVZaKategorijuAvans, x.SifraKategorijePDVAvans, x.UkupanIznosPDVKodAvansneFakture, x.ReferencaNaPrethodnuFakturu }).Select(y => new FinaInvoice_TaxSubtotalAvans
            {
                IznosPDVOsnoviceAvans = y.Sum(a => a.IznosPDVOsnoviceAvans),
                IznosPorezaZaKategorijuPDVAvans = y.Sum(a => a.IznosPorezaZaKategorijuPDVAvans),
                UkupanIznosPDVKodAvansneFakture = y.Sum(a => a.UkupanIznosPDVKodAvansneFakture),
                StopaPDVZaKategorijuAvans = y.Key.StopaPDVZaKategorijuAvans,
                SifraKategorijePDVAvans = y.Key.SifraKategorijePDVAvans,
                /* SifraRazlogaZaoslobodjenjeOdPDVAvans = y.Key.SifraRazlogaZaoslobodjenjeOdPDVAvans,
                 RazlogZaOslobodjenjeOdPDVAvans = y.Key.RazlogZaOslobodjenjeOdPDVAvans,*/
                ReferencaNaPrethodnuFakturu = y.Key.ReferencaNaPrethodnuFakturu



            }).ToList();

            foreach (var item in ListSubtotalPrepaymentAmmount)
            {
                XmlElement InvoicedPrepaymentAmmount = xml_doc.CreateElement("xsd", "InvoicedPrepaymentAmmount", xsd_ns);
                TaxTotalInvoicedPrepaymentAmmount.AppendChild(InvoicedPrepaymentAmmount);

                XmlElement InvoicedPrepaymentAmmountID = xml_doc.CreateElement("cbc", "ID", cbc_ns);
                InvoicedPrepaymentAmmountID.InnerText = item.ReferencaNaPrethodnuFakturu.ReferencaNaPrethodnuFakturu.ToString();
                InvoicedPrepaymentAmmount.AppendChild(InvoicedPrepaymentAmmountID);


                XmlElement TaxTotalElement = xml_doc.CreateElement("cac", "TaxTotal", cac_ns);
                InvoicedPrepaymentAmmount.AppendChild(TaxTotalElement);

                XmlElement TaxTotalAmounElement = xml_doc.CreateElement("cbc", "TaxAmount", cbc_ns);
                TaxTotalAmounElement.InnerText = item.UkupanIznosPDVKodAvansneFakture.ToString();
                TaxTotalAmounElement.SetAttribute("currencyID", request.SifraValuteFakture);
                TaxTotalElement.AppendChild(TaxTotalAmounElement);

                XmlElement TaxSubtotalElement = xml_doc.CreateElement("cac", "TaxSubtotal", cac_ns);
                TaxTotalElement.AppendChild(TaxSubtotalElement);

                XmlElement TaxableAmountElement = xml_doc.CreateElement("cbc", "TaxableAmount", cbc_ns);
                TaxableAmountElement.InnerText = item.IznosPDVOsnoviceAvans.ToString();
                TaxableAmountElement.SetAttribute("currencyID", request.SifraValuteFakture);
                TaxSubtotalElement.AppendChild(TaxableAmountElement);


                XmlElement TaxAmountElement = xml_doc.CreateElement("cbc", "TaxAmount", cbc_ns);
                TaxAmountElement.InnerText = item.IznosPorezaZaKategorijuPDVAvans.ToString();
                TaxAmountElement.SetAttribute("currencyID", request.SifraValuteFakture);
                TaxSubtotalElement.AppendChild(TaxAmountElement);


                XmlElement TaxCategoryElement = xml_doc.CreateElement("cac", "TaxCategory", cac_ns);
                TaxSubtotalElement.AppendChild(TaxCategoryElement);


                XmlElement TaxCategoryIDElement = xml_doc.CreateElement("cbc", "ID", cbc_ns);
                TaxCategoryIDElement.InnerText = item.SifraKategorijePDVAvans.ToString();
                TaxCategoryElement.AppendChild(TaxCategoryIDElement);


                XmlElement PercentElement = xml_doc.CreateElement("cbc", "Percent", cbc_ns);
                PercentElement.InnerText = item.StopaPDVZaKategorijuAvans.ToString();
                TaxCategoryElement.AppendChild(PercentElement);

                XmlElement TaxSchemeElement = xml_doc.CreateElement("cac", "TaxScheme", cac_ns);
                TaxCategoryElement.AppendChild(TaxSchemeElement);

                XmlElement TaxSchemeIDElement = xml_doc.CreateElement("cbc", "ID", cbc_ns);
                TaxSchemeIDElement.InnerText = "VAT";
                TaxSchemeElement.AppendChild(TaxSchemeIDElement);

            }



            XmlNode ReducedTotalsTaxTotals = xml_doc.SelectSingleNode("//inv:Invoice/cec:UBLExtensions/cec:UBLExtension/cec:ExtensionContent/sbt:SrbDtExt", namespaces);

            XmlElement ReducedTotalsElement = xml_doc.CreateElement("xsd", "ReducedTotals", xsd_ns);
            ReducedTotalsTaxTotals.AppendChild(ReducedTotalsElement);

            XmlElement TaxTotalElement1 = xml_doc.CreateElement("cac", "TaxTotal", cac_ns);
            ReducedTotalsElement.AppendChild(TaxTotalElement1);

            XmlElement TaxTotalAmounElement1 = xml_doc.CreateElement("cbc", "TaxAmount", cbc_ns);
            TaxTotalAmounElement1.InnerText = request.SmanjeniUkupanIznosPDVKodAvansneFakture.ToString();
            TaxTotalAmounElement1.SetAttribute("currencyID", request.SifraValuteFakture);
            TaxTotalElement1.AppendChild(TaxTotalAmounElement1);

            List<FinaInvoice_TaxSubtotalAvansReduced> ListSubtotalReducedTotals = request.PoreskiMeduzbirAvansReduced.GroupBy(x => new { x.StopaPDVZaKategorijuAvansReduced, x.SifraKategorijePDVAvansReduced }).Select(y => new FinaInvoice_TaxSubtotalAvansReduced
            {
                IznosPDVOsnoviceAvansReduced = y.Sum(a => a.IznosPDVOsnoviceAvansReduced),
                IznosPorezaZaKategorijuPDVAvansReduced = y.Sum(a => a.IznosPorezaZaKategorijuPDVAvansReduced),
                StopaPDVZaKategorijuAvansReduced = y.Key.StopaPDVZaKategorijuAvansReduced,
                SifraKategorijePDVAvansReduced = y.Key.SifraKategorijePDVAvansReduced

            }).ToList();

            foreach (var item in ListSubtotalReducedTotals)
            {


                XmlElement TaxSubtotalElement = xml_doc.CreateElement("cac", "TaxSubtotal", cac_ns);
                TaxTotalElement1.AppendChild(TaxSubtotalElement);

                XmlElement TaxableAmountElement = xml_doc.CreateElement("cbc", "TaxableAmount", cbc_ns);
                TaxableAmountElement.InnerText = item.IznosPDVOsnoviceAvansReduced.ToString();
                TaxableAmountElement.SetAttribute("currencyID", request.SifraValuteFakture);
                TaxSubtotalElement.AppendChild(TaxableAmountElement);


                XmlElement TaxAmountElement = xml_doc.CreateElement("cbc", "TaxAmount", cbc_ns);
                TaxAmountElement.InnerText = item.IznosPorezaZaKategorijuPDVAvansReduced.ToString();
                TaxAmountElement.SetAttribute("currencyID", request.SifraValuteFakture);
                TaxSubtotalElement.AppendChild(TaxAmountElement);


                XmlElement TaxCategoryElement = xml_doc.CreateElement("cac", "TaxCategory", cac_ns);
                TaxSubtotalElement.AppendChild(TaxCategoryElement);


                XmlElement TaxCategoryIDElement = xml_doc.CreateElement("cbc", "ID", cbc_ns);
                TaxCategoryIDElement.InnerText = item.SifraKategorijePDVAvansReduced.ToString();
                TaxCategoryElement.AppendChild(TaxCategoryIDElement);


                XmlElement PercentElement = xml_doc.CreateElement("cbc", "Percent", cbc_ns);
                PercentElement.InnerText = item.StopaPDVZaKategorijuAvansReduced.ToString();
                TaxCategoryElement.AppendChild(PercentElement);

                XmlElement TaxSchemeElement = xml_doc.CreateElement("cac", "TaxScheme", cac_ns);
                TaxCategoryElement.AppendChild(TaxSchemeElement);

                XmlElement TaxSchemeIDElement = xml_doc.CreateElement("cbc", "ID", cbc_ns);
                TaxSchemeIDElement.InnerText = "VAT";
                TaxSchemeElement.AppendChild(TaxSchemeIDElement);


            }

            /* XmlNode SmanjeniUkupanIznosPDVKodAvansneFakture = xml_doc.SelectSingleNode("//inv:Invoice/cec:UBLExtensions/cec:UBLExtension/cec:ExtensionContent/sbt:SrbDtExt/xsd:ReducedTotals/cac:TaxTotal/cbc:TaxAmount", namespaces);
             SmanjeniUkupanIznosPDVKodAvansneFakture.InnerText = request.SmanjeniUkupanIznosPDVKodAvansneFakture.ToString();*/

            // XmlNode SmanjeniUkupanIznosPDVKodAvansneFakture = xml_doc.SelectSingleNode("//inv:Invoice/cec:UBLExtensions/cec:UBLExtension/cec:ExtensionContent/sbt:SrbDtExt/xsd:ReducedTotals/cac:TaxTotal/cbc:TaxAmount", namespaces);
            //  SmanjeniUkupanIznosPDVKodAvansneFakture.InnerText = request.SmanjeniUkupanIznosPDVKodAvansneFakture.ToString();

            // XmlNode ReducedTotalsTaxTotals2 = xml_doc.SelectSingleNode("//xsd:ReducedTotals", namespaces);

            XmlNode LegalMonetaryTotal = xml_doc.CreateElement("cac", "LegalMonetaryTotal", cac_ns);
            ReducedTotalsElement.AppendChild(LegalMonetaryTotal);

            XmlElement TaxExclusiveAmount = xml_doc.CreateElement("cbc", "TaxExclusiveAmount", cbc_ns);
            TaxExclusiveAmount.InnerText = request.SumarnoAvans.UkupanIznosBezPDVAvans.ToString();
            TaxExclusiveAmount.SetAttribute("currencyID", request.SifraValuteFakture);
            LegalMonetaryTotal.AppendChild(TaxExclusiveAmount);

            XmlElement TaxInclusiveAmount = xml_doc.CreateElement("cbc", "TaxInclusiveAmount", cbc_ns);
            TaxInclusiveAmount.InnerText = request.SumarnoAvans.UkupanIznosSaPDVAvans.ToString();
            TaxInclusiveAmount.SetAttribute("currencyID", request.SifraValuteFakture);
            LegalMonetaryTotal.AppendChild(TaxInclusiveAmount);

            XmlElement PayableAmount = xml_doc.CreateElement("cbc", "PayableAmount", cbc_ns);
            PayableAmount.InnerText = request.SumarnoAvans.IznosZaPlacanjeAvans.ToString();
            PayableAmount.SetAttribute("currencyID", request.SifraValuteFakture);
            LegalMonetaryTotal.AppendChild(PayableAmount);

            ////// AVANSI

            XmlNode BrojFakture = xml_doc.SelectSingleNode("//inv:Invoice/cbc:ID", namespaces);
            BrojFakture.InnerText = request.BrojFakture;

            XmlNode DatumIzdavanjaRacuna = xml_doc.SelectSingleNode("//inv:Invoice/cbc:IssueDate", namespaces);
            DatumIzdavanjaRacuna.InnerText = request.DatumIzdavanjaRacuna.ToString("yyyy-MM-dd");

            XmlNode DatumDospecaPlacanja = xml_doc.SelectSingleNode("//inv:Invoice/cbc:DueDate", namespaces);
            DatumDospecaPlacanja.InnerText = request.DatumDospecaPlacanja.ToString("yyyy-MM-dd");

            XmlNode SifraVrsteFakture = xml_doc.SelectSingleNode("//inv:Invoice/cbc:InvoiceTypeCode", namespaces);
            SifraVrsteFakture.InnerText = request.SifraVrsteFakture;

            XmlNode SifraValuteFakture = xml_doc.SelectSingleNode("//inv:Invoice/cbc:DocumentCurrencyCode", namespaces);
            SifraValuteFakture.InnerText = request.SifraValuteFakture;

            XmlNode InterniBroj = xml_doc.SelectSingleNode("//inv:Invoice/cbc:BuyerReference", namespaces);
            InterniBroj.InnerText = request.InterniBroj;

            XmlNode SifraDatumaPoreskeObaveze = xml_doc.SelectSingleNode("//inv:Invoice/cac:InvoicePeriod/cbc:DescriptionCode", namespaces);
            SifraDatumaPoreskeObaveze.InnerText = request.SifraDatumaPoreskeObaveze;//proveriti sta je ovo


            XmlNode BrojNarudzbenice = xml_doc.SelectSingleNode("//inv:Invoice/cac:OrderReference/cbc:ID", namespaces);
            BrojNarudzbenice.InnerText = request.BrojNarudzbenice;

            XmlNode BrojOtpremnice = xml_doc.SelectSingleNode("//inv:Invoice/cac:DespatchDocumentReference/cbc:ID", namespaces);
            BrojOtpremnice.InnerText = request.BrojOtpremnice;

            // XmlNode BillingRefercence = xml_doc.SelectSingleNode("//inv:Invoice", namespaces);

            foreach (var item in request.ReferencaNaPrethodnuFakturu)
            {

                XmlNode RootElement = xml_doc.SelectSingleNode("//inv:Invoice", namespaces);
                XmlNode RefContract = xml_doc.SelectSingleNode("//inv:Invoice/cac:OrderReference", namespaces);
                XmlNode BillingRefercenceElement = xml_doc.CreateElement("cac", "BillingReference", cac_ns);
                RootElement.InsertAfter(BillingRefercenceElement, RefContract);

                XmlElement InvoiceDocumentReference = xml_doc.CreateElement("cac", "InvoiceDocumentReference", cac_ns);
                BillingRefercenceElement.AppendChild(InvoiceDocumentReference);

                XmlElement InvoiceDocumentReferenceID = xml_doc.CreateElement("cbc", "ID", cbc_ns);
                InvoiceDocumentReferenceID.InnerText = item.ReferencaNaPrethodnuFakturu.ToString();
                InvoiceDocumentReference.AppendChild(InvoiceDocumentReferenceID);

                // Ovde dodati datum DatumIzdavanjaPredhodnogRacuna

            }


            XmlNode BrojOkvirnogSporazuma = xml_doc.SelectSingleNode("//inv:Invoice/cac:OriginatorDocumentReference/cbc:ID", namespaces);
            BrojOkvirnogSporazuma.InnerText = request.BrojOkvirnogSporazuma;


            XmlNode BrojUgovora = xml_doc.SelectSingleNode("//inv:Invoice/cac:ContractDocumentReference/cbc:ID", namespaces);
            BrojUgovora.InnerText = request.BrojUgovora;

            if (request.PrilozeniPDF.Count <= 3)
            {

                foreach (var line1 in request.PrilozeniPDF.Select((value1, n) => new { n, value1 }))
                {


                    XmlNode RootElement = xml_doc.SelectSingleNode("//inv:Invoice", namespaces);
                    XmlNode RefContract = xml_doc.SelectSingleNode("//inv:Invoice/cac:ContractDocumentReference", namespaces);
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

            #region podaci o prodavcu

            XmlNode PIBProdavcaEndpointID = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingSupplierParty/cac:Party/cbc:EndpointID", namespaces);
            PIBProdavcaEndpointID.InnerText = request.PIBProdavca;

            XmlNode IdentifikatorProdavca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification", namespaces);
            if (request.IdentifikatorProdavca == null)
            {
                XmlNode AccountingSupplierParty_Party = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingSupplierParty/cac:Party", namespaces);
                AccountingSupplierParty_Party.RemoveChild(IdentifikatorProdavca);
            }
            else
            {
                IdentifikatorProdavca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyIdentification/cbc:ID", namespaces);
                IdentifikatorProdavca.InnerText = "JBKJS:" + request.IdentifikatorProdavca;
            }

            XmlNode TrgovackiNazivProdavca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name", namespaces);
            TrgovackiNazivProdavca.InnerText = request.TrgovackiNazivProdavca;

            XmlNode MestoProdavca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:CityName", namespaces);
            MestoProdavca.InnerText = request.MestoProdavca;

            XmlNode SifraDrzaveProdavca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cac:Country/cbc:IdentificationCode", namespaces);
            SifraDrzaveProdavca.InnerText = request.SifraDrzaveProdavca;

            // XmlNode NazivDrzaveProdavca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cac:Country/cbc:Name", namespaces);
            //  NazivDrzaveProdavca.InnerText = request.NazivDrzaveProdavca; //Ovo nema u PDF-u, ali ima node u XML


            XmlNode PIBProdavcaCompanyID = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme/cbc:CompanyID", namespaces);
            PIBProdavcaCompanyID.InnerText = "RS" + request.PIBProdavca;

            XmlNode ProdavacEvidencijaPDV = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme/cac:TaxScheme/cbc:ID", namespaces);
            ProdavacEvidencijaPDV.InnerText = request.ProdavacEvidencijaPDV == true ? "VAT" : "NO-VAT";

            XmlNode PoslovnoImeProdavca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:RegistrationName", namespaces);
            PoslovnoImeProdavca.InnerText = request.PoslovnoImeProdavca;

            XmlNode MaticniBrojProdavca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID", namespaces);
            MaticniBrojProdavca.InnerText = request.MaticniBrojProdavca;

            XmlNode EMailAdresaProdavca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingSupplierParty/cac:Party/cac:Contact/cbc:ElectronicMail", namespaces);
            EMailAdresaProdavca.InnerText = request.EMailAdresaProdavca;

            #endregion

            #region podaci o kupcu

            XmlNode PIBKupcaEndpointID = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingCustomerParty/cac:Party/cbc:EndpointID", namespaces);
            PIBKupcaEndpointID.InnerText = request.PIBKupca;

            XmlNode IdentifikatorKupca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyIdentification", namespaces);
            if (request.IdentifikatorKupca == null)
            {
                XmlNode AccountingCustomerParty_Party = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingCustomerParty/cac:Party", namespaces);
                AccountingCustomerParty_Party.RemoveChild(IdentifikatorKupca);
            }
            else
            {
                IdentifikatorKupca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyIdentification/cbc:ID", namespaces);
                IdentifikatorKupca.InnerText = "JBKJS:" + request.IdentifikatorKupca;
            }

            XmlNode TrgovackiNazivKupca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name", namespaces);
            TrgovackiNazivKupca.InnerText = request.TrgovackiNazivKupca;

            XmlNode UlicaKupca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:StreetName", namespaces);
            UlicaKupca.InnerText = request.UlicaKupca;

            XmlNode MestoKupca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:CityName", namespaces);
            MestoKupca.InnerText = request.MestoKupca;

            XmlNode PostanskiBrojKupca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:PostalZone", namespaces);
            PostanskiBrojKupca.InnerText = request.PostanskiBrojKupca;

            XmlNode SifraDrzaveKupca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cac:Country/cbc:IdentificationCode", namespaces);
            SifraDrzaveKupca.InnerText = request.SifraDrzaveKupca;

            //    XmlNode NazivDrzaveKupca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cac:Country/cbc:Name", namespaces);
            //  NazivDrzaveKupca.InnerText = request.NazivDrzaveKupca;

            XmlNode PIBKupcaCompanyID = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyTaxScheme/cbc:CompanyID", namespaces);
            PIBKupcaCompanyID.InnerText = "RS" + request.PIBKupca;

            XmlNode KupacEvidencijaPDV = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyTaxScheme/cac:TaxScheme/cbc:ID", namespaces);
            KupacEvidencijaPDV.InnerText = request.KupacEvidencijaPDV == true ? "VAT" : "NO-VAT"; //Ovo nema u PDF-u, ali ima node u XML

            XmlNode PoslovnoImeKupca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity/cbc:RegistrationName", namespaces);
            PoslovnoImeKupca.InnerText = request.PoslovnoImeKupca;

            XmlNode MaticniBrojKupca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID", namespaces);
            MaticniBrojKupca.InnerText = request.MaticniBrojKupca;

            XmlNode EMailAdresaKupca = xml_doc.SelectSingleNode("//inv:Invoice/cac:AccountingCustomerParty/cac:Party/cac:Contact/cbc:ElectronicMail", namespaces);
            EMailAdresaKupca.InnerText = request.EMailAdresaKupca;

            #endregion

            XmlNode StvarniDatumIsporuke = xml_doc.SelectSingleNode("//inv:Invoice/cac:Delivery/cbc:ActualDeliveryDate", namespaces);
            StvarniDatumIsporuke.InnerText = request.StvarniDatumIsporuke.ToString("yyyy-MM-dd");

            XmlNode SifraNacinaPlacanja = xml_doc.SelectSingleNode("//inv:Invoice/cac:PaymentMeans/cbc:PaymentMeansCode", namespaces);
            SifraNacinaPlacanja.InnerText = request.SifraNacinaPlacanja.ToString();

            XmlNode PozivNaBroj = xml_doc.SelectSingleNode("//inv:Invoice/cac:PaymentMeans/cbc:PaymentID", namespaces);
            PozivNaBroj.InnerText = request.PozivNaBroj.ToString();

            XmlNode TekuciRacun = xml_doc.SelectSingleNode("//inv:Invoice/cac:PaymentMeans/cac:PayeeFinancialAccount/cbc:ID", namespaces);
            TekuciRacun.InnerText = request.TekuciRacun.ToString();

            XmlNode UkupanIznosPDV = xml_doc.SelectSingleNode("//inv:Invoice/cac:TaxTotal/cbc:TaxAmount", namespaces);
            UkupanIznosPDV.InnerText = request.UkupanIznosPDV.ToString();

            List<TaxSubtotal> ListSubtotalTaxTotal = request.PoreskiMeduzbir.GroupBy(x => new { x.StopaPDVZaKategoriju, x.SifraKategorijePDV, x.SifraRazlogaZaoslobodjenjeOdPDV, x.RazlogZaOslobodjenjeOdPDV }).Select(y => new TaxSubtotal
            {
                IznosPDVOsnovice = y.Sum(a => a.IznosPDVOsnovice),
                IznosPorezaZaKategorijuPDV = y.Sum(a => a.IznosPorezaZaKategorijuPDV),
                StopaPDVZaKategoriju = y.Key.StopaPDVZaKategoriju,
                SifraKategorijePDV = y.Key.SifraKategorijePDV,
                SifraRazlogaZaoslobodjenjeOdPDV = y.Key.SifraRazlogaZaoslobodjenjeOdPDV,
                RazlogZaOslobodjenjeOdPDV = y.Key.RazlogZaOslobodjenjeOdPDV

            }).ToList();

            XmlNode TaxTotal = xml_doc.SelectSingleNode("//inv:Invoice/cac:TaxTotal", namespaces);

            foreach (var item in ListSubtotalTaxTotal)
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

            XmlNode ZbirNetoIznosaIzStavkiRacuna = xml_doc.SelectSingleNode("//inv:Invoice/cac:LegalMonetaryTotal/cbc:LineExtensionAmount", namespaces);
            ZbirNetoIznosaIzStavkiRacuna.InnerText = request.Sumarno.ZbirNetoIznosaIzStavkiRacuna.ToString();

            XmlNode UkupanIznosBezPDV = xml_doc.SelectSingleNode("//inv:Invoice/cac:LegalMonetaryTotal/cbc:TaxExclusiveAmount", namespaces);
            UkupanIznosBezPDV.InnerText = request.Sumarno.UkupanIznosBezPDV.ToString();

            XmlNode UkupanIznosSaPDV = xml_doc.SelectSingleNode("//inv:Invoice/cac:LegalMonetaryTotal/cbc:TaxInclusiveAmount", namespaces);
            UkupanIznosSaPDV.InnerText = request.Sumarno.UkupanIznosSaPDV.ToString();

            XmlNode ZbirPopustaNaNivouDokumenta = xml_doc.SelectSingleNode("//inv:Invoice/cac:LegalMonetaryTotal/cbc:AllowanceTotalAmount", namespaces);
            ZbirPopustaNaNivouDokumenta.InnerText = request.Sumarno.ZbirPopustaNaNivouDokumenta.ToString();

            XmlNode PlacenIznos = xml_doc.SelectSingleNode("//inv:Invoice/cac:LegalMonetaryTotal/cbc:PrepaidAmount", namespaces);
            PlacenIznos.InnerText = request.Sumarno.PlacenIznos.ToString();//dodaoo ovo

            XmlNode IznosZaPlacanje = xml_doc.SelectSingleNode("//inv:Invoice/cac:LegalMonetaryTotal/cbc:PayableAmount", namespaces);
            IznosZaPlacanje.InnerText = request.Sumarno.IznosZaPlacanje.ToString();


            foreach (var line in request.StavkeFakture.Select((value, i) => new { i, value }))
            {
                XmlElement InvoiceLineElement = xml_doc.CreateElement("cac", "InvoiceLine", cac_ns);
                RootNode.AppendChild(InvoiceLineElement);

                XmlElement InvoiceLineIDElement = xml_doc.CreateElement("cbc", "ID", cbc_ns);
                InvoiceLineIDElement.InnerText = (line.i + 1).ToString();
                InvoiceLineElement.AppendChild(InvoiceLineIDElement);

                XmlElement InvoicedQuantityElement = xml_doc.CreateElement("cbc", "InvoicedQuantity", cbc_ns);
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



            Console.WriteLine(xml_doc.OuterXml);

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
        public string EmailHeaderTemplateCompaniyRegistrationTrue(Reques_FinalInvoice body)
        {

            string htmlBody = "";
            htmlBody += "<html><tbody>";
           

            return htmlBody;
        }
        #endregion EmailHeaderTemplateCompaniyRegistrationTrue

        #region EmailHeaderTemplateCompaniyRegistrationFalse
        public string EmailHeaderTemplateCompaniyRegistrationFalse(Reques_FinalInvoice body)
        {
            string htmlBody = "";
            htmlBody += "<html><tbody>";
          

            return htmlBody;
        }
        #endregion EmailHeaderTemplateCompaniyRegistrationFalse

        public async Task SendEmail(Reques_FinalInvoice body)
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
