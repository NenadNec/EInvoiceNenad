using eInvoice.Core.Models;
using eInvoice.Core.Models.ERP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Infrastructure.Repositories.Interfaces
{
    public interface IFinalInvoiceRepository
    {
        string ReturnString();
        string FillFinalInvoice(Reques_FinalInvoice request);
        Task<CompanyAccountOnEfAkturaDto> CheckIfCompanyRegisteredOnEfaktura(CompanyAccountIdentificationDto bodyCheck);
        Task SendEmail(Reques_FinalInvoice body);
    }
}