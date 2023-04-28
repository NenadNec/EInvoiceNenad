using eInvoice.Infrastructure.Repositories;
using eInvoice.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        IBaseRepository _baseRepository;
        ISalesInvoiceRepository _salesInvoice;
        IPurchaseInvoiceRepository _purchaseInvoice;
        ICreditNoteRepository _creditNote;
        IFinalInvoiceRepository _finalInvoice;

        public UnitOfWork()
        {

        }
        public IBaseRepository BaseRepository
        {
            get
            {
                if (_baseRepository == null)
                    _baseRepository = new BaseRepository();

                return _baseRepository;
            }
        }

        public ISalesInvoiceRepository SalesInvoice
        {
            get
            {
                if (_salesInvoice == null)
                    _salesInvoice = new SalesInvoiceRepository();

                return _salesInvoice;
            }
        }

        public IFinalInvoiceRepository FinalInvoice
        {
            get
            {
                if (_finalInvoice == null)
                    _finalInvoice = new FinalInvoiceRepository();

                return _finalInvoice;
            }
        }


        public IPurchaseInvoiceRepository PurchaseInvoice
        {
            get
            {
                if (_purchaseInvoice == null)
                    _purchaseInvoice = new PurchaseInvoiceRepository();
                return _purchaseInvoice;
            }
        }

        public ICreditNoteRepository CreditNote
        {
            get
            {
                if (_creditNote == null)
                    _creditNote = new CreditNoteRepository();

                return _creditNote;
            }
        }

    }
}
