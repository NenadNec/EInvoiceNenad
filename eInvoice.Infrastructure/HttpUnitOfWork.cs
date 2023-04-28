using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Infrastructure
{
    public class HttpUnitOfWork : UnitOfWork
    {
        public HttpUnitOfWork(IHttpContextAccessor httpAccessor)
        {
        }
    }
}
