using System;
using System.Collections.Generic;
using eInvoice.Core.Models.ERP;
using Newtonsoft.Json;

public class ReturnEnvDocumentPdf
{

    [JsonProperty("env:DocumentEnvelope")]
    public EnvDocumentEnvelopeDocumentReturn EnvDocumentEnvelope { get; set; }
}

public class EnvDocumentEnvelopeDocumentReturn
{

    [JsonProperty("env:DocumentHeader")]
    public EnvDocumentHeaderDocumentReturn EnvDocumentHeader { get; set; }

    [JsonProperty("env:DocumentBody")]
    public EnvDocumentBodyReturn EnvDocumentBody { get; set; }

}

public class EnvDocumentHeaderDocumentReturn
{
    [JsonProperty("env:SalesInvoiceId")]
    public string EnvSalesInvoiceId { get; set; }

    [JsonProperty("env:PurchaseInvoiceId")]
    public string EnvPurchaseInvoiceId { get; set; }

    /* [JsonProperty("env:DocumentId")]
     public string EnvDocumentId { get; set; }*/

    [JsonProperty("env:CreationDate")]
    public string EnvCreationDate { get; set; }

    [JsonProperty("env:SendingDate")]
    public string EnvSendingDate { get; set; }

    [JsonProperty("env:DocumentPdf")]
    public EnvDocumentPdfDocumentReturn EnvDocumentPdf { get; set; }
}

public class EnvDocumentPdfDocumentReturn
{

    [JsonProperty("#text")]
    public string Text { get; set; }
}

public class CacAttachmentReturn
{
    [JsonProperty("cbc:EmbeddedDocumentBinaryObject")]
    public CbcEmbeddedDocumentBinaryObjectReturn CbcEmbeddedDocumentBinaryObject { get; set; }
}

public class CbcEmbeddedDocumentBinaryObjectReturn
{
    [JsonProperty("@mimeCode")]
    public string MimeCode { get; set; }

    [JsonProperty("#text")]
    public string Text { get; set; }
}

public class CacAdditionalDocumentReferenceReturn
{
    [JsonProperty("cbc:ID")]
    public string CbcID { get; set; }

    [JsonProperty("cac:Attachment")]
    public CacAttachmentReturn CacAttachment { get; set; }
}

public class InvoiceReturn
{
    [JsonProperty("cac:AdditionalDocumentReference")]
    [JsonConverter(typeof(SingleValueArrayConverter<CacAdditionalDocumentReferenceReturn>))]
    public CacAdditionalDocumentReferenceReturn[] CacAdditionalDocumentReference { get; set; }
}

public class EnvDocumentBodyReturn
{
    public InvoiceReturn Invoice { get; set; }
}