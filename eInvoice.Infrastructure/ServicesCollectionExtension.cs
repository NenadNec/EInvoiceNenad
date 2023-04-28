using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Infrastructure
{
    public static class ServicesCollectionExtension
    {
        public static IServiceCollection AddInfrastructurServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, HttpUnitOfWork>();

            return services;
        }
    }
}
