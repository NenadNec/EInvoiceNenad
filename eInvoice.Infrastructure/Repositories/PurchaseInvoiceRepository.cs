using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using eInvoice.Core.Models;
using eInvoice.Core.Models.ERP;
using eInvoice.Infrastructure.Repositories.Interfaces;
using Newtonsoft.Json;

namespace eInvoice.Infrastructure.Repositories
{
    public class PurchaseInvoiceRepository : BaseRepository, IPurchaseInvoiceRepository
    {
        public PurchaseInvoiceRepository()
        { }

        public async Task<List<PurchaseInvoiceStatusChangeDto>> PurchaseInvoiceStatusLastDay(string api_key)
        {
            List<PurchaseInvoiceStatusChangeDto> result = new List<PurchaseInvoiceStatusChangeDto>();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/purchase-invoice/changes?date=" + DateTime.Now.AddDays(-1));
            request.Headers.Add("apikey", api_key);
            request.Headers.Add("Accept", "appliaction/json");

            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string response_string = await response.Content.ReadAsStringAsync();
                if (response_string != "[]")
                {

                    result = JsonConvert.DeserializeObject<List<PurchaseInvoiceStatusChangeDto>>(response_string);

                }

                return result;
            }
            else
            {
                string error_string = await response.Content.ReadAsStringAsync();
                Error_Model error = (Error_Model)JsonConvert.DeserializeObject(error_string, typeof(Error_Model));

                return null;
            }

        }

        public async Task<List<PurchaseInvoiceStatusChangeDto>> GetChangedPurchaseInvoice(string apikey)
        {
            List<PurchaseInvoiceStatusChangeDto> result = new List<PurchaseInvoiceStatusChangeDto>();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/purchase-invoice/changes?date=" + DateTime.Now.AddDays(-1));
            request.Headers.Add("apikey", apikey);
            request.Headers.Add("Accept", "appliaction/json");

            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string response_string = await response.Content.ReadAsStringAsync();
                if (response_string != "[]")
                {
                    result = JsonConvert.DeserializeObject<List<PurchaseInvoiceStatusChangeDto>>(response_string).GroupBy(x => x.purchaseInvoiceId)
                    .SelectMany(a => a.OrderByDescending(c => c.date).Take(1)).ToList();

                }

                return result;
            }
            else
            {
                string error_string = await response.Content.ReadAsStringAsync();
                Error_Model error = (Error_Model)JsonConvert.DeserializeObject(error_string, typeof(Error_Model));
                return result;

            }

        }

        public async Task<GroupInvoice> JsonFormatPurchaseInvoice(string apikey, PurchaseInvoiceStatusChangeDto item)
        {


            GroupInvoice groupInvoice = new GroupInvoice();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/purchase-invoice/xml?invoiceId=" + item.purchaseInvoiceId);
            request.Headers.Add("ApiKey", apikey);
            request.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {

                string response_string = await response.Content.ReadAsStringAsync();

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response_string);

                string fromXml = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);

                if (fromXml != "")
                {
                    RootAllInvoice fromJsonAdvance = JsonConvert.DeserializeObject<RootAllInvoice>(fromXml);
                    groupInvoice.InvoiceAll = fromJsonAdvance;
                }


                return groupInvoice;

            }
            else
            {
                string error_string = await response.Content.ReadAsStringAsync();
                Error_Model error = (Error_Model)JsonConvert.DeserializeObject(error_string, typeof(Error_Model));
                return null;

            }

        }


        public async Task<XmlFormatInvoice> XmlFormatPurchaseInvoice(string apikey, PurchaseInvoiceStatusChangeDto item)
        {

            XmlFormatInvoice objInvoiceXml = new XmlFormatInvoice();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/purchase-invoice/xml?invoiceId=" + item.purchaseInvoiceId);
            request.Headers.Add("ApiKey", apikey);
            request.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {

                string response_string = await response.Content.ReadAsStringAsync();
                objInvoiceXml.InvoiceXML = response_string.Replace("\r\n", "").Replace(" ", "");

                return objInvoiceXml;
            }
            else
            {
                string error_string = await response.Content.ReadAsStringAsync();
                Error_Model error = (Error_Model)JsonConvert.DeserializeObject(error_string, typeof(Error_Model));
                return null;
            }

        }

        public async Task<ReturnEnvDocumentPdf> PurchaseInvoicePdf(string api_key, PurchaseInvoiceStatusChangeDto item)
        {
            List<ReturnEnvDocumentPdf> invoicePdf = new List<ReturnEnvDocumentPdf>();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/purchase-invoice/xml?invoiceId=" + item.purchaseInvoiceId);
            request.Headers.Add("ApiKey", api_key);
            request.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string response_string = await response.Content.ReadAsStringAsync();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response_string);

                string fromXml = Newtonsoft.Json.JsonConvert.SerializeObject(doc);
                ReturnEnvDocumentPdf fromJson = JsonConvert.DeserializeObject<ReturnEnvDocumentPdf>(fromXml);

                return fromJson;
            }
            else
            {
                string error_string = await response.Content.ReadAsStringAsync();
                Error_Model error = (Error_Model)JsonConvert.DeserializeObject(error_string, typeof(Error_Model));
                return null;
            }

        }









    }
}