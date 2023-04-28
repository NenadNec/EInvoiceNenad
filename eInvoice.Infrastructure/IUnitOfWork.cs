using eInvoice.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Infrastructure
{
    public interface IUnitOfWork
    {
        IBaseRepository BaseRepository { get; }
        ISalesInvoiceRepository SalesInvoice { get; }
        IPurchaseInvoiceRepository PurchaseInvoice { get; }
        ICreditNoteRepository CreditNote { get; }
        IFinalInvoiceRepository FinalInvoice{get; }

    }
}
