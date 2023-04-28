using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using eInvoice.Core.Models;
using eInvoice.Core.Models.ERP;

namespace eInvoice.Infrastructure.Repositories.Interfaces
{
    public interface IPurchaseInvoiceRepository
    {
        Task<List<PurchaseInvoiceStatusChangeDto>> GetChangedPurchaseInvoice(string apikey);
        Task<GroupInvoice> JsonFormatPurchaseInvoice(string api_key, PurchaseInvoiceStatusChangeDto item);
        Task<List<PurchaseInvoiceStatusChangeDto>> PurchaseInvoiceStatusLastDay(string api_key);
        Task<XmlFormatInvoice> XmlFormatPurchaseInvoice (string api_key, PurchaseInvoiceStatusChangeDto item);
        Task<ReturnEnvDocumentPdf> PurchaseInvoicePdf (string api_key, PurchaseInvoiceStatusChangeDto item);
    }
}