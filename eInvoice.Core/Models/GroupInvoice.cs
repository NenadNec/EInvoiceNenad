// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System;
using System.Collections.Generic;
using eInvoice.Core.Models.ERP;

public class GroupInvoice
{
    public RootAllInvoice InvoiceAll { get; set; }

}