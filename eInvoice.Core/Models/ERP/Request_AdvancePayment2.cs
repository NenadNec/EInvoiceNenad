/*using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace eInvoice.Core.Models.ERP
{
    public class Request_AdvancePayment2 : AuditableModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CbcNote
    {
        [JsonProperty("-self-closing")]
        public string SelfClosing { get; set; }
    }

    public class CacInvoicePeriod
    {
        [JsonProperty("cbc:DescriptionCode")]
        public string CbcDescriptionCode { get; set; }
    }

    public class CacContractDocumentReference
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }
    }

    public class CbcEndpointID
    {
        [JsonProperty("-schemeID")]
        public string SchemeID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CacPartyName
    {
        [JsonProperty("cbc:Name")]
        public string CbcName { get; set; }
    }

    public class CacCountry
    {
        [JsonProperty("cbc:IdentificationCode")]
        public string CbcIdentificationCode { get; set; }
    }

    public class CacPostalAddress
    {
        [JsonProperty("cbc:CityName")]
        public string CbcCityName { get; set; }

        [JsonProperty("cac:Country")]
        public CacCountry CacCountry { get; set; }

        [JsonProperty("cbc:StreetName")]
        public string CbcStreetName { get; set; }

        [JsonProperty("cbc:PostalZone")]
        public string CbcPostalZone { get; set; }
    }

    public class CacTaxScheme
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }
    }

    public class CacPartyTaxScheme
    {
        [JsonProperty("cbc:CompanyID")]
        public string CbcCompanyID { get; set; }

        [JsonProperty("cac:TaxScheme")]
        public CacTaxScheme CacTaxScheme { get; set; }
    }

    public class CacPartyLegalEntity
    {
        [JsonProperty("cbc:RegistrationName")]
        public string CbcRegistrationName { get; set; }

        [JsonProperty("cbc:CompanyID")]
        public string CbcCompanyID { get; set; }
    }

    public class CacParty
    {
        [JsonProperty("cbc:EndpointID")]
        public CbcEndpointID CbcEndpointID { get; set; }

        [JsonProperty("cac:PartyName")]
        public CacPartyName CacPartyName { get; set; }

        [JsonProperty("cac:PostalAddress")]
        public CacPostalAddress CacPostalAddress { get; set; }

        [JsonProperty("cac:PartyTaxScheme")]
        public CacPartyTaxScheme CacPartyTaxScheme { get; set; }

        [JsonProperty("cac:PartyLegalEntity")]
        public CacPartyLegalEntity CacPartyLegalEntity { get; set; }

        [JsonProperty("cac:PartyIdentification")]
        public CacPartyIdentification CacPartyIdentification { get; set; }
    }

    public class CacAccountingSupplierParty
    {
        [JsonProperty("cac:Party")]
        public CacParty CacParty { get; set; }
    }

    public class CacPartyIdentification
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }
    }

    public class CacAccountingCustomerParty
    {
        [JsonProperty("cac:Party")]
        public CacParty CacParty { get; set; }
    }

    public class CbcPaymentID
    {
        [JsonProperty("-self-closing")]
        public string SelfClosing { get; set; }
    }

    public class CacPayeeFinancialAccount
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }
    }

    public class CacPaymentMeans
    {
        [JsonProperty("cbc:PaymentMeansCode")]
        public string CbcPaymentMeansCode { get; set; }

        [JsonProperty("cbc:PaymentID")]
        public CbcPaymentID CbcPaymentID { get; set; }

        [JsonProperty("cac:PayeeFinancialAccount")]
        public CacPayeeFinancialAccount CacPayeeFinancialAccount { get; set; }
    }

    public class CbcTaxAmount
    {
        [JsonProperty("-currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CbcTaxableAmount
    {
        [JsonProperty("-currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CacTaxCategory
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }

        [JsonProperty("cbc:Percent")]
        public string CbcPercent { get; set; }

        [JsonProperty("cac:TaxScheme")]
        public CacTaxScheme CacTaxScheme { get; set; }
    }

    public class CacTaxSubtotal
    {
        [JsonProperty("cbc:TaxableAmount")]
        public CbcTaxableAmount CbcTaxableAmount { get; set; }

        [JsonProperty("cbc:TaxAmount")]
        public CbcTaxAmount CbcTaxAmount { get; set; }

        [JsonProperty("cac:TaxCategory")]
        public CacTaxCategory CacTaxCategory { get; set; }
    }

    public class CacTaxTotal
    {
        [JsonProperty("cbc:TaxAmount")]
        public CbcTaxAmount CbcTaxAmount { get; set; }

        [JsonProperty("cac:TaxSubtotal")]
        public CacTaxSubtotal CacTaxSubtotal { get; set; }
    }

    public class CbcLineExtensionAmount
    {
        [JsonProperty("-currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CbcTaxExclusiveAmount
    {
        [JsonProperty("-currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CbcTaxInclusiveAmount
    {
        [JsonProperty("-currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CbcAllowanceTotalAmount
    {
        [JsonProperty("-currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CbcPrepaidAmount
    {
        [JsonProperty("-currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CbcPayableAmount
    {
        [JsonProperty("-currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CacLegalMonetaryTotal
    {
        [JsonProperty("cbc:LineExtensionAmount")]
        public CbcLineExtensionAmount CbcLineExtensionAmount { get; set; }

        [JsonProperty("cbc:TaxExclusiveAmount")]
        public CbcTaxExclusiveAmount CbcTaxExclusiveAmount { get; set; }

        [JsonProperty("cbc:TaxInclusiveAmount")]
        public CbcTaxInclusiveAmount CbcTaxInclusiveAmount { get; set; }

        [JsonProperty("cbc:AllowanceTotalAmount")]
        public CbcAllowanceTotalAmount CbcAllowanceTotalAmount { get; set; }

        [JsonProperty("cbc:PrepaidAmount")]
        public CbcPrepaidAmount CbcPrepaidAmount { get; set; }

        [JsonProperty("cbc:PayableAmount")]
        public CbcPayableAmount CbcPayableAmount { get; set; }
    }

    public class CbcInvoicedQuantity
    {
        [JsonProperty("-unitCode")]
        public string UnitCode { get; set; }

        [JsonProperty("-unitCodeListID")]
        public string UnitCodeListID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CacSellersItemIdentification
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }
    }

    public class CacClassifiedTaxCategory
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }

        [JsonProperty("cbc:Percent")]
        public string CbcPercent { get; set; }

        [JsonProperty("cac:TaxScheme")]
        public CacTaxScheme CacTaxScheme { get; set; }
    }

    public class CacItem
    {
        [JsonProperty("cbc:Name")]
        public string CbcName { get; set; }

        [JsonProperty("cac:SellersItemIdentification")]
        public CacSellersItemIdentification CacSellersItemIdentification { get; set; }

        [JsonProperty("cac:ClassifiedTaxCategory")]
        public CacClassifiedTaxCategory CacClassifiedTaxCategory { get; set; }
    }

    public class CbcPriceAmount
    {
        [JsonProperty("-currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CacPrice
    {
        [JsonProperty("cbc:PriceAmount")]
        public CbcPriceAmount CbcPriceAmount { get; set; }
    }

    public class CacInvoiceLine
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }

        [JsonProperty("cbc:InvoicedQuantity")]
        public CbcInvoicedQuantity CbcInvoicedQuantity { get; set; }

        [JsonProperty("cbc:LineExtensionAmount")]
        public CbcLineExtensionAmount CbcLineExtensionAmount { get; set; }

        [JsonProperty("cac:Item")]
        public CacItem CacItem { get; set; }

        [JsonProperty("cac:Price")]
        public CacPrice CacPrice { get; set; }
    }

    public class Invoice
    {
        [JsonProperty("-xmlns:cac")]
        public string XmlnsCac { get; set; }

        [JsonProperty("-xmlns:cbc")]
        public string XmlnsCbc { get; set; }

        [JsonProperty("-xmlns:xsi")]
        public string XmlnsXsi { get; set; }

        [JsonProperty("-xmlns:xsd")]
        public string XmlnsXsd { get; set; }

        [JsonProperty("-xmlns")]
        public string Xmlns { get; set; }

        [JsonProperty("cbc:CustomizationID")]
        public string CbcCustomizationID { get; set; }

        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }

        [JsonProperty("cbc:IssueDate")]
        public string CbcIssueDate { get; set; }

        [JsonProperty("cbc:DueDate")]
        public string CbcDueDate { get; set; }

        [JsonProperty("cbc:InvoiceTypeCode")]
        public string CbcInvoiceTypeCode { get; set; }

        [JsonProperty("cbc:Note")]
        public CbcNote CbcNote { get; set; }

        [JsonProperty("cbc:DocumentCurrencyCode")]
        public string CbcDocumentCurrencyCode { get; set; }

        [JsonProperty("cac:InvoicePeriod")]
        public CacInvoicePeriod CacInvoicePeriod { get; set; }

        [JsonProperty("cac:ContractDocumentReference")]
        public CacContractDocumentReference CacContractDocumentReference { get; set; }

        [JsonProperty("cac:AccountingSupplierParty")]
        public CacAccountingSupplierParty CacAccountingSupplierParty { get; set; }

        [JsonProperty("cac:AccountingCustomerParty")]
        public CacAccountingCustomerParty CacAccountingCustomerParty { get; set; }

        [JsonProperty("cac:PaymentMeans")]
        public CacPaymentMeans CacPaymentMeans { get; set; }

        [JsonProperty("cac:TaxTotal")]
        public CacTaxTotal CacTaxTotal { get; set; }

        [JsonProperty("cac:LegalMonetaryTotal")]
        public CacLegalMonetaryTotal CacLegalMonetaryTotal { get; set; }

        [JsonProperty("cac:InvoiceLine")]
        public CacInvoiceLine CacInvoiceLine { get; set; }
    }

    public class Root
    {
        public Invoice Invoice { get; set; }
    }


    }
}*/