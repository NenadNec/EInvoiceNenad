using eInvoice.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace eInvoice.Controllers.api
{
    //[Authorize]
    [ApiController]
    [ServiceFilter(typeof(ApiExceptionFilter))]
    [Route("api/[controller]")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [AllowAnonymous]
    public class BaseController : Controller
    {
        public static string server_url = Startup.Configuration["ServerUrl"];
        public static string logFile_path = Startup.Configuration["LogFilePath"];
        public static string ubl_storage = Startup.Configuration["UblStorage"];
        public HttpClient httpClient = new HttpClient( new HttpClientHandler(){

            SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls13 | SslProtocols.Tls,
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

        });
        public BaseController()
        {
        }
    }
}
