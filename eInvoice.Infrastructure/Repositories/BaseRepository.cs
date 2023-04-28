using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using eInvoice.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace eInvoice.Infrastructure.Repositories
{

    public class BaseRepository : IBaseRepository
    {
        static ConfigurationBuilder builder = new ConfigurationBuilder();
        static IConfiguration Configuration = builder.AddJsonFile("appsettings.json").Build();
        public string server_url = Configuration["ServerUrl"];
        public string logFile_path = Configuration["LogFilePath"];
        public string ubl_storage = Configuration["UblStorage"];

         public HttpClient httpClient = new HttpClient( new HttpClientHandler(){

            SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls13 | SslProtocols.Tls,
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

        });

        public void WriteToLogFile(string message, string path)
        {
            TextWriter tw = new StreamWriter(path, true);
            tw.WriteLine(DateTime.Now.ToString() + " " + message);
            tw.Close();
        }
    }
}