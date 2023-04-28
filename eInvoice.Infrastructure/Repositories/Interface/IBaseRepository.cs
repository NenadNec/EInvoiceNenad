using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eInvoice.Core.Models.ERP;

namespace eInvoice.Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository
    {
        void WriteToLogFile(string message, string path);
    }
}