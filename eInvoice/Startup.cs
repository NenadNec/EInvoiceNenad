using eInvoice.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eInvoice
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "E-fakture", Version = "v1" });
            });
            services.RegisterCustomServices();
            services.AddRouting(options => options.LowercaseUrls = false);

            services.AddSwaggerGen(c => { c.EnableAnnotations(); });

            //kada se radi DeserializeObject pretvara enum u string umesto u int
            services.AddControllers(x => x.AllowEmptyInputInBodyModelBinding = true).AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "eInvoice v1"));
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/e-faktura/swagger/v1/swagger.json", "E-faktura v1"));
            }
            //odkomentarisati kad hocemo da vidimo swagger na hetzneru
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/e-faktura/swagger/v1/swagger.json", "E-faktura v1"));
            //odkomentarisati kad hocemo da vidimo swagger na hetzneru

            app.UseHttpsRedirection(); //when comment https is disables

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
