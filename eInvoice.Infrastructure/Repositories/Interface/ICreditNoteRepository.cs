using eInvoice.Core.Models;
using eInvoice.Core.Models.ERP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Infrastructure.Repositories.Interfaces
{
    public interface ICreditNoteRepository
    {
        string FillCreditNote(Request_CreditNote2 request);
        Task<CompanyAccountOnEfAkturaDto> CheckIfCompanyRegisteredOnEfaktura(CompanyAccountIdentificationDto bodyCheck);
        Task SendEmail(Request_CreditNote2 body);
    }
}
