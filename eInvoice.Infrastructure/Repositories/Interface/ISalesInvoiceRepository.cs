using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eInvoice.Core.Models;
using eInvoice.Core.Models.ERP;

namespace eInvoice.Infrastructure.Repositories.Interfaces
{
    public interface ISalesInvoiceRepository
    {

        string ReturnString();
        string FillSalesUbl(Request_ImportSalesUbl request);
        Task<List<SalesInvoiceStatusChangeDto>> GetChangedSalesInvoice(string apikey);
        Task<GroupInvoice> JsonFormatSalesInvoice(string api_key, SalesInvoiceStatusChangeDto item);
        Task<List<SalesInvoiceStatusChangeDto>> SalesInvoiceStatusLastDay(string api_key);
        Task<XmlFormatInvoice> XmlFormatSalesInvoice (string api_key, SalesInvoiceStatusChangeDto item);
        Task<ReturnEnvDocumentPdf> InvoiceDocumentPdf (string api_key , SalesInvoiceStatusChangeDto item);
        Task<CompanyAccountOnEfAkturaDto> CheckIfCompanyRegisteredOnEfaktura (CompanyAccountIdentificationDto bodyCheck);
        Task SendEmail(Request_ImportSalesUbl body);
    }
}
