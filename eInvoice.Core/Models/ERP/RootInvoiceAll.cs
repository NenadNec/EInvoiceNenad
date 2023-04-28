using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using eInvoice.Core.Models.ERP;
using Newtonsoft.Json;

namespace eInvoice.Core.Models.ERP
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CacAccountingCustomerPartyRootInvoiceAll
    {
        [JsonProperty("cac:Party")]
        public CacPartyRootInvoiceAll CacParty { get; set; }
    }

    public class CacAccountingSupplierPartyRootInvoiceAll
    {
        [JsonProperty("cac:Party")]
        public CacPartyRootInvoiceAll CacParty { get; set; }
    }

    public class CacAllowanceChargeRootInvoiceAll
    {
        [JsonProperty("cbc:ChargeIndicator")]
        public string CbcChargeIndicator { get; set; }

        [JsonProperty("cbc:MultiplierFactorNumeric")]
        public string CbcMultiplierFactorNumeric { get; set; }

        [JsonProperty("cbc:Amount")]
        public CbcAmountRootInvoiceAll CbcAmount { get; set; }
    }

    public class CacBillingReferenceRootInvoiceAll
    {
        [JsonProperty("cac:InvoiceDocumentReference")]
        public CacInvoiceDocumentReferenceRootInvoiceAll CacInvoiceDocumentReference { get; set; }
    }

    public class CacClassifiedTaxCategoryRootInvoiceAll
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }

        [JsonProperty("cbc:Percent")]
        public string CbcPercent { get; set; }

        [JsonProperty("cac:TaxScheme")]
        public CacTaxSchemeRootInvoiceAll CacTaxScheme { get; set; }
    }


    public class CacContactRootInvoiceAll
    {
        [JsonProperty("cbc:ElectronicMail")]
        public string CbcElectronicMail { get; set; }
    }

    public class CacContractDocumentReferenceRootInvoiceAll
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }
    }

    public class CacCountryRootInvoiceAll
    {
        [JsonProperty("cbc:IdentificationCode")]
        public string CbcIdentificationCode { get; set; }
    }

    public class CacDeliveryRootInvoiceAll
    {
        [JsonProperty("cbc:ActualDeliveryDate")]
        public string CbcActualDeliveryDate { get; set; }
    }

    public class CacInvoiceDocumentReferenceRootInvoiceAll
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }

        [JsonProperty("cbc:IssueDate")]
        public string CbcIssueDate { get; set; }
    }

    public class CacInvoiceLineRootInvoiceAll
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }

        [JsonProperty("cbc:InvoicedQuantity")]
        public CbcInvoicedQuantityRootInvoiceAll CbcInvoicedQuantity { get; set; }

        [JsonProperty("cbc:CreditedQuantity")]
        public CbcInvoicedQuantityRootInvoiceAll CbcCreditedQuantity { get; set; }

        [JsonProperty("cbc:LineExtensionAmount")]
        public CbcLineExtensionAmountRootInvoiceAll CbcLineExtensionAmount { get; set; }

        [JsonProperty("cac:Item")]
        public CacItemRootInvoiceAll CacItem { get; set; }

        [JsonProperty("cac:Price")]
        public CacPriceRootInvoiceAll CacPrice { get; set; }

        [JsonProperty("cac:AllowanceCharge")]
        public CacAllowanceChargeRootInvoiceAll CacAllowanceCharge { get; set; }
    }

    /*public class CacCreditNoteLine
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }

        [JsonProperty("cbc:CreditedQuantity")]
        public CbcInvoicedQuantityRootInvoiceAll CbcCreditedQuantity { get; set; }

        [JsonProperty("cbc:LineExtensionAmount")]
        public CbcLineExtensionAmountRootInvoiceAll CbcLineExtensionAmount { get; set; }

        [JsonProperty("cac:Item")]
        public CacItemRootInvoiceAll CacItem { get; set; }

        [JsonProperty("cac:Price")]
        public CacPriceRootInvoiceAll CacPrice { get; set; }

        [JsonProperty("cac:AllowanceCharge")]
        public CacAllowanceChargeRootInvoiceAll CacAllowanceCharge { get; set; }
    }*/

    public class CacInvoicePeriodRootInvoiceAll
    {
        [JsonProperty("cbc:DescriptionCode")]
        public string CbcDescriptionCode { get; set; }
    }

    public class CacItemRootInvoiceAll
    {
        [JsonProperty("cbc:Name")]
        public string CbcName { get; set; }

        [JsonProperty("cac:SellersItemIdentification")]
        public CacSellersItemIdentificationRootInvoiceAll CacSellersItemIdentification { get; set; }

        [JsonProperty("cac:ClassifiedTaxCategory")]
        public CacClassifiedTaxCategoryRootInvoiceAll CacClassifiedTaxCategory { get; set; }
    }

    public class CacLegalMonetaryTotalRootInvoiceAll
    {
        [JsonProperty("cbc:LineExtensionAmount")]
        public CbcLineExtensionAmountRootInvoiceAll CbcLineExtensionAmount { get; set; }

        [JsonProperty("cbc:TaxExclusiveAmount")]
        public CbcTaxExclusiveAmountRootInvoiceAll CbcTaxExclusiveAmount { get; set; }

        [JsonProperty("cbc:TaxInclusiveAmount")]
        public CbcTaxInclusiveAmountRootInvoiceAll CbcTaxInclusiveAmount { get; set; }

        [JsonProperty("cbc:AllowanceTotalAmount")]
        public CbcAllowanceTotalAmountRootInvoiceAll CbcAllowanceTotalAmount { get; set; }

        [JsonProperty("cbc:PrepaidAmount")]
        public CbcPrepaidAmountRootInvoiceAll CbcPrepaidAmount { get; set; }

        [JsonProperty("cbc:PayableAmount")]
        public CbcPayableAmountRootInvoiceAll CbcPayableAmount { get; set; }
    }

    public class CacPartyRootInvoiceAll
    {
        [JsonProperty("cbc:EndpointID")]
        public CbcEndpointIDRootInvoiceAll CbcEndpointID { get; set; }

        [JsonProperty("cac:PartyName")]
        public CacPartyNameRootInvoiceAll CacPartyName { get; set; }

        [JsonProperty("cac:PostalAddress")]
        public CacPostalAddressRootInvoiceAll CacPostalAddress { get; set; }

        [JsonProperty("cac:PartyTaxScheme")]
        public CacPartyTaxSchemeRootInvoiceAll CacPartyTaxScheme { get; set; }

        [JsonProperty("cac:PartyLegalEntity")]
        public CacPartyLegalEntityRootInvoiceAll CacPartyLegalEntity { get; set; }

        [JsonProperty("cac:Contact")]
        public CacContactRootInvoiceAll CacContact { get; set; }

        [JsonProperty("cac:PartyIdentification")]
        public CacPartyIdentificationRootInvoiceAll CacPartyIdentification { get; set; }
    }

    public class CacPartyIdentificationRootInvoiceAll
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }
    }

    public class CacPartyLegalEntityRootInvoiceAll
    {
        [JsonProperty("cbc:RegistrationName")]
        public string CbcRegistrationName { get; set; }

        [JsonProperty("cbc:CompanyID")]
        public string CbcCompanyID { get; set; }
    }

    public class CacPartyNameRootInvoiceAll
    {
        [JsonProperty("cbc:Name")]
        public string CbcName { get; set; }
    }

    public class CacPartyTaxSchemeRootInvoiceAll
    {
        [JsonProperty("cbc:CompanyID")]
        public string CbcCompanyID { get; set; }

        [JsonProperty("cac:TaxScheme")]
        public CacTaxSchemeRootInvoiceAll CacTaxScheme { get; set; }
    }

    public class CacPayeeFinancialAccountRootInvoiceAll
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }
    }

    public class CacPaymentMeansRootInvoiceAll
    {
        [JsonProperty("cbc:PaymentMeansCode")]
        public string CbcPaymentMeansCode { get; set; }

        [JsonProperty("cbc:PaymentID")]
        public string paymentID { get; set; }

        [JsonProperty("cac:PayeeFinancialAccount")]
        public CacPayeeFinancialAccountRootInvoiceAll CacPayeeFinancialAccount { get; set; }
    }

    public class CacPostalAddressRootInvoiceAll
    {
        [JsonProperty("cbc:CityName")]
        public string CbcCityName { get; set; }

        [JsonProperty("cac:Country")]
        public CacCountryRootInvoiceAll CacCountry { get; set; }

        [JsonProperty("cbc:StreetName")]
        public string CbcStreetName { get; set; }

        [JsonProperty("cbc:PostalZone")]
        public string CbcPostalZone { get; set; }
    }

    public class CacPriceRootInvoiceAll
    {
        [JsonProperty("cbc:PriceAmount")]
        public CbcPriceAmountRootInvoiceAll CbcPriceAmount { get; set; }
    }

    public class CacSellersItemIdentificationRootInvoiceAll
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }
    }

    public class CacTaxCategoryRootInvoiceAll
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }

        [JsonProperty("cbc:Percent")]
        public string CbcPercent { get; set; }

        [JsonProperty("cac:TaxScheme")]
        public CacTaxSchemeRootInvoiceAll CacTaxScheme { get; set; }

        [JsonProperty("cbc:TaxExemptionReasonCode")]
        public string CbcTaxExemptionReasonCode { get; set; }

        [JsonProperty("cbc:TaxExemptionReason")]
        public string CbcTaxExemptionReason { get; set; }
    }

    public class CacTaxSchemeRootInvoiceAll
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }
    }

    public class CacOriginatorDocumentReferenceRootInvoiceAll
    {
        [JsonProperty("cbc:ID")]
        public string cbcID { get; set; }
    }

    public class CacOrderReferenceRootInvoiceAll
    {
        [JsonProperty("cbc:ID")]
        public string cbcID { get; set; }
    }

    public class CacTaxSubtotalRootInvoiceAll
    {
        [JsonProperty("cbc:TaxableAmount")]
        public CbcTaxableAmountRootInvoiceAll CbcTaxableAmount { get; set; }

        [JsonProperty("cbc:TaxAmount")]
        public CbcTaxAmountRootInvoiceAll CbcTaxAmount { get; set; }

        [JsonProperty("cac:TaxCategory")]
        public CacTaxCategoryRootInvoiceAll CacTaxCategory { get; set; }

    }

    public class CacTaxTotalRootInvoiceAll
    {
        [JsonProperty("cbc:TaxAmount")]
        public CbcTaxAmountRootInvoiceAll CbcTaxAmount { get; set; }

        [JsonProperty("cac:TaxSubtotal")]
        [JsonConverter(typeof(SingleValueArrayConverter<CacTaxSubtotalRootInvoiceAll>))]
        public CacTaxSubtotalRootInvoiceAll[] CacTaxSubtotal { get; set; }
    }

    public class CbcAllowanceTotalAmountRootInvoiceAll
    {
        [JsonProperty("@currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CbcAmountRootInvoiceAll
    {
        [JsonProperty("@currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CbcEndpointIDRootInvoiceAll
    {
        [JsonProperty("@schemeID")]
        public string SchemeID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CbcInvoicedQuantityRootInvoiceAll
    {
        [JsonProperty("@unitCode")]
        public string UnitCode { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CbcLineExtensionAmountRootInvoiceAll
    {
        [JsonProperty("@currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CbcPayableAmountRootInvoiceAll
    {
        [JsonProperty("@currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CbcPrepaidAmountRootInvoiceAll
    {
        [JsonProperty("@currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CbcPriceAmountRootInvoiceAll
    {
        [JsonProperty("@currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CbcTaxableAmountRootInvoiceAll
    {
        [JsonProperty("@currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CbcTaxAmountRootInvoiceAll
    {
        [JsonProperty("@currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CbcTaxExclusiveAmountRootInvoiceAll
    {
        [JsonProperty("@currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class CbcTaxInclusiveAmountRootInvoiceAll
    {
        [JsonProperty("@currencyID")]
        public string CurrencyID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class EnvDocumentBodyRootInvoiceAll
    {

        [JsonProperty("Invoice")]
        public InvoiceRootInvoiceAll Invoice { get; set; }

        [JsonProperty("CreditNote")]
        public InvoiceRootInvoiceAll CreditNote { get; set; }
    }

    public class EnvDocumentEnvelopeRootInvoiceAll
    {

        [JsonProperty("env:DocumentHeader")]
        public EnvDocumentHeaderRootInvoiceAll EnvDocumentHeader { get; set; }

        [JsonProperty("env:DocumentBody")]
        public EnvDocumentBodyRootInvoiceAll EnvDocumentBody { get; set; }
    }

    public class EnvDocumentHeaderRootInvoiceAll
    {
        [JsonProperty("env:SalesInvoiceId")]
        public string EnvSalesInvoiceId { get; set; }

        [JsonProperty("env:PurchaseInvoiceId")]
        public string EnvPurchaseInvoiceId { get; set; }

        [JsonProperty("env:DocumentId")]
        public string EnvDocumentId { get; set; }

        [JsonProperty("env:CreationDate")]
        public string EnvCreationDate { get; set; }

        [JsonProperty("env:SendingDate")]
        public string EnvSendingDate { get; set; }

    }


    public class InvoiceRootInvoiceAll
    {

        [JsonProperty("cec:UBLExtensions")]
        public CecUBLExtensionsRootInvoiceAll CecUBLExtensions { get; set; }

        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }

        [JsonProperty("cbc:IssueDate")]
        public string CbcIssueDate { get; set; }

        [JsonProperty("cbc:DueDate")]
        public string CbcDueDate { get; set; }

        [JsonProperty("cbc:InvoiceTypeCode")]
        public string CbcInvoiceTypeCode { get; set; }

        [JsonProperty("cbc:CreditNoteTypeCode")]
        public string CbcCreditNoteTypeCode { get; set; }

        [JsonProperty("cbc:Note")]
        public string CbcNote { get; set; }

        [JsonProperty("cbc:DocumentCurrencyCode")]
        public string CbcDocumentCurrencyCode { get; set; }

        [JsonProperty("cac:OrderReference")]
        public CacOrderReferenceRootInvoiceAll cacOrderReference { get; set; }

        [JsonProperty("cbc:BuyerReference")]
        public string CbcBuyerReference { get; set; }

        [JsonProperty("cac:InvoicePeriod")]
        public CacInvoicePeriodRootInvoiceAll CacInvoicePeriod { get; set; }

        [JsonProperty("cac:BillingReference")]
        [JsonConverter(typeof(SingleValueArrayConverter<CacBillingReferenceRootInvoiceAll>))]
        public CacBillingReferenceRootInvoiceAll[] CacBillingReference { get; set; }

        [JsonProperty("cac:ContractDocumentReference")]
        public CacContractDocumentReferenceRootInvoiceAll CacContractDocumentReference { get; set; }

        [JsonProperty("cac:AccountingSupplierParty")]
        public CacAccountingSupplierPartyRootInvoiceAll CacAccountingSupplierParty { get; set; }

        [JsonProperty("cac:AccountingCustomerParty")]
        public CacAccountingCustomerPartyRootInvoiceAll CacAccountingCustomerParty { get; set; }

        [JsonProperty("cac:Delivery")]
        public CacDeliveryRootInvoiceAll CacDelivery { get; set; }

        [JsonProperty("cac:OriginatorDocumentReference")]
        public CacOriginatorDocumentReferenceRootInvoiceAll cacOriginatorDocumentReference { get; set; }

        [JsonProperty("cac:PaymentMeans")]
        public CacPaymentMeansRootInvoiceAll CacPaymentMeans { get; set; }

        [JsonProperty("cac:TaxTotal")]
        public CacTaxTotalRootInvoiceAll CacTaxTotal { get; set; }

        [JsonProperty("cac:LegalMonetaryTotal")]
        public CacLegalMonetaryTotalRootInvoiceAll CacLegalMonetaryTotal { get; set; }

        [JsonProperty("cac:CreditNoteLine")]
        [JsonConverter(typeof(SingleValueArrayConverter<CacInvoiceLineRootInvoiceAll>))]
        public CacInvoiceLineRootInvoiceAll[] CacCreditNoteLine { get; set; }

        [JsonProperty("cac:InvoiceLine")]
        [JsonConverter(typeof(SingleValueArrayConverter<CacInvoiceLineRootInvoiceAll>))]
        public CacInvoiceLineRootInvoiceAll[] CacInvoiceLine { get; set; }
    }

    public class RootAllInvoice
    {

        [JsonProperty("env:DocumentEnvelope")]
        public EnvDocumentEnvelopeRootInvoiceAll EnvDocumentEnvelope { get; set; }
    }

    public class CecUBLExtensionsRootInvoiceAll
    {
        [JsonProperty("cec:UBLExtension")]
        public CecUBLExtensionRootInvoiceAll CecUBLExtension { get; set; }
    }
    public class CecUBLExtensionRootInvoiceAll
    {
        [JsonProperty("cec:ExtensionContent")]
        public CecExtensionContentRootInvoiceAll CecExtensionContent { get; set; }
    }

    public class CecExtensionContentRootInvoiceAll
    {
        [JsonProperty("sbt:SrbDtExt")]
        public SbtSrbDtExtRootInvoiceAll SbtSrbDtExt { get; set; }
    }

    public class SbtSrbDtExtRootInvoiceAll
    {
        [JsonProperty("xsd:InvoicedPrepaymentAmmount")]
        [JsonConverter(typeof(SingleValueArrayConverter<XsdInvoicedPrepaymentAmmountRootInvoiceAll>))]
        public XsdInvoicedPrepaymentAmmountRootInvoiceAll[] XsdInvoicedPrepaymentAmmount { get; set; }

        [JsonProperty("xsd:ReducedTotals")]
        public XsdReducedTotalsRootInvoiceAll XsdReducedTotals { get; set; }
    }

    public class XsdInvoicedPrepaymentAmmountRootInvoiceAll
    {
        [JsonProperty("cbc:ID")]
        public string CbcID { get; set; }

        [JsonProperty("cac:TaxTotal")]
        public CacTaxTotalRootInvoiceAll CacTaxTotal { get; set; }
    }

    public class XsdReducedTotalsRootInvoiceAll
    {
        [JsonProperty("cac:TaxTotal")]
        public CacTaxTotalRootInvoiceAll CacTaxTotal { get; set; }

        [JsonProperty("cac:LegalMonetaryTotal")]
        public CacLegalMonetaryTotalRootInvoiceAll CacLegalMonetaryTotal { get; set; }
    }

    public class SingleValueArrayConverter<T> : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject
                || reader.TokenType == JsonToken.String
                || reader.TokenType == JsonToken.Integer)
            {
                return new T[] { serializer.Deserialize<T>(reader) };
            }
            return serializer.Deserialize<T[]>(reader);
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }

}