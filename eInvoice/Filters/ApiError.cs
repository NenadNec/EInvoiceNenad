using eInvoice.Filters;
using eInvoice.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eInvoice.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
        {
            // New instance every time, only configuration class needs so its ok
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ApiExceptionFilter>();
            services.AddInfrastructurServices();
            return services;
        }
    }
}
