using eInvoice.Controllers.api;
using eInvoice.Core.Models;
using eInvoice.Core.Models.ERP;
using eInvoice.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace eInvoice.Controllers
{

    public class PurchaseInvoiceController : BaseController
    {

        private readonly IUnitOfWork _uow;
        public PurchaseInvoiceController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        List<GroupInvoice> inv = new List<GroupInvoice>();

        [ProducesResponseType(typeof(SimpleSalesInvoiceDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Get purchase invoice")]
        [HttpGet()]
        public async Task<ActionResult> GetPurchaseInvoice([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] int invoiceId)
        {
            try
            {
                SimplePurchaseInvoiceDto result = new SimplePurchaseInvoiceDto();


                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/purchase-invoice?invoiceId=" + invoiceId);
                request.Headers.Add("apikey", api_key);
                request.Headers.Add("Accept", "application/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = (SimplePurchaseInvoiceDto)JsonConvert.DeserializeObject(response_string, typeof(SimplePurchaseInvoiceDto));
                    return Ok(result);
                }
                else
                {
                    string error_string = await response.Content.ReadAsStringAsync();
                    Error_Model error = (Error_Model)JsonConvert.DeserializeObject(error_string, typeof(Error_Model));

                    return StatusCode((int)response.StatusCode, error);
                }

            }

            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }




        //[ProducesResponseType(typeof(PurchaseInvoiceStatusChangeDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Search for all purchase invoices which status changed on specific date")]
        [HttpPost("changes")]
        public async Task<ActionResult> SearchForAllPurchaseInvoicesWhichStatusChangedOnSpecificDate([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] string date)
        {
            List<PurchaseInvoiceStatusChangeDto> result = new List<PurchaseInvoiceStatusChangeDto>();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/purchase-invoice/changes?date=" + date);
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

                return Ok(result);
            }
            else
            {
                string error_string = await response.Content.ReadAsStringAsync();
                Error_Model error = (Error_Model)JsonConvert.DeserializeObject(error_string, typeof(Error_Model));

                return StatusCode((int)response.StatusCode, error);
            }

        }


        [ProducesResponseType(typeof(PurchaseInvoiceStatusChangeDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Search for all purchase invoices which status changed on last day")]
        [HttpPost("lastDay")]
        public async Task<ActionResult> PurchaseInvoiceStatusLastDay([FromHeader(Name = "ApiKey")][Required] string api_key)
        {
            try
            {

                var result = await _uow.PurchaseInvoice.PurchaseInvoiceStatusLastDay(api_key);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

        }



        [ProducesResponseType(typeof(AcceptRejectPurchaseInvoiceResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Accept or reject purchase invoice")]
        [HttpPost("acceptRejectPurchaseInvoice")]
        public async Task<IActionResult> AcceptOrRejectPurchaseInvoice([FromHeader(Name = "ApiKey")][Required] string api_key, [FromBody] AcceptRejectPurchaseInvoice body)
        {
            try
            {
                AcceptRejectPurchaseInvoiceResponse result = new AcceptRejectPurchaseInvoiceResponse();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/purchase-invoice/acceptRejectPurchaseInvoice");
                request.Headers.Add("apikey", api_key);
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");


                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<AcceptRejectPurchaseInvoiceResponse>(response_string);
                    return Ok(result);
                }
                else
                {
                    string error_string = await response.Content.ReadAsStringAsync();
                    Error_Model error = (Error_Model)JsonConvert.DeserializeObject(error_string, typeof(Error_Model));

                    return StatusCode((int)response.StatusCode, error);
                }

            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        //proveriti sta tacno radi ovaj API
        [SwaggerOperation(Summary = "Changes status of purchase invoices based on FitekIn status updates")]
        [HttpPost("fitekInStatusUpdate")]
        public async Task<IActionResult> ChangesStatusOfPurchaseInvoicesBasedOnFitekInStatusUpdates([FromBody] StatusChangeRequest body)
        {
            try
            {
                StatusChangeRequest result = new StatusChangeRequest();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/purchase-invoice/fitekInStatusUpdate");
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");


                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // string response_string = await response.Content.ReadAsStringAsync();
                    // result = JsonConvert.DeserializeObject<StatusChangeRequest>(response_string);
                    return Ok();
                }
                else
                {
                    string error_string = await response.Content.ReadAsStringAsync();
                    Error_Model error = (Error_Model)JsonConvert.DeserializeObject(error_string, typeof(Error_Model));

                    return StatusCode((int)response.StatusCode, error);
                }

            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }


        [ProducesResponseType(typeof(SearchPurchaseInvoiceResultDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Search CIR invoices")]
        [HttpPost("findCirInvoices")]
        public async Task<IActionResult> FindCirInvoices([FromHeader(Name = "ApiKey")][Required] string api_key, [FromBody] SearchParameter body)
        {
            try
            {
                SearchPurchaseInvoiceResultDto result = new SearchPurchaseInvoiceResultDto();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/purchase-invoice/findCirInvoices");
                request.Headers.Add("apikey", api_key);
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");


                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<SearchPurchaseInvoiceResultDto>(response_string);
                    return Ok(result);
                }
                else
                {
                    string error_string = await response.Content.ReadAsStringAsync();
                    Error_Model error = (Error_Model)JsonConvert.DeserializeObject(error_string, typeof(Error_Model));

                    return StatusCode((int)response.StatusCode, error);
                }

            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [ProducesResponseType(typeof(PurchaseInvoiceDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Assign Cir invoice")]
        [HttpPost("{cirInvoiceId}/assign")]
        public async Task<IActionResult> AssignCirInvoice([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] string cirInvoiceId, [Required] string AssignerPartyJBKJS, [Required] string AssignationContractNumber)
        {
            try
            {
                PurchaseInvoiceDto result = new PurchaseInvoiceDto();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/purchase-invoice/" + cirInvoiceId + "/assign?AssignerPartyJBKJS=" + AssignerPartyJBKJS + "&AssignationContractNumber=" + AssignationContractNumber);
                request.Headers.Add("apikey", api_key);
                request.Headers.Add("Accept", "application/json");


                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<PurchaseInvoiceDto>(response_string);
                    return Ok(result);
                }
                else
                {
                    string error_string = await response.Content.ReadAsStringAsync();
                    Error_Model error = (Error_Model)JsonConvert.DeserializeObject(error_string, typeof(Error_Model));

                    return StatusCode((int)response.StatusCode, error);
                }

            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }


        [ProducesResponseType(typeof(PurchaseInvoiceDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Cancel Cir invoice assignment")]
        [HttpGet("{cirInvoiceId}/cancelassign")]
        public async Task<ActionResult> CancelCirInvoiceAssignment([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] int cirInvoiceId)
        {
            try
            {
                PurchaseInvoiceDto result = new PurchaseInvoiceDto();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/purchase-invoice/" + cirInvoiceId + "/cancelassign");
                request.Headers.Add("apikey", api_key);
                request.Headers.Add("Accept", "application/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = (PurchaseInvoiceDto)JsonConvert.DeserializeObject(response_string, typeof(PurchaseInvoiceDto));
                    return Ok(result);
                }
                else
                {
                    string error_string = await response.Content.ReadAsStringAsync();
                    Error_Model error = (Error_Model)JsonConvert.DeserializeObject(error_string, typeof(Error_Model));

                    return StatusCode((int)response.StatusCode, error);
                }

            }

            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Accept or reject purchase invoice by CIR invoice id")]
        [HttpPost("acceptRejectPurchaseInvoiceByCirInvoiceId")]
        public async Task<IActionResult> AcceptOrRejectPurchaseInvoice([FromHeader(Name = "ApiKey")][Required] string api_key, [FromBody] AcceptRejectPurchaseInvoiceByCirInvoiceId body)
        {
            try
            {
                AcceptRejectPurchaseInvoiceResponse result = new AcceptRejectPurchaseInvoiceResponse();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/purchase-invoice/acceptRejectPurchaseInvoiceByCirInvoiceId");
                request.Headers.Add("apikey", api_key);
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<AcceptRejectPurchaseInvoiceResponse>(response_string);
                    return Ok(result);
                }
                else
                {
                    string error_string = await response.Content.ReadAsStringAsync();
                    Error_Model error = (Error_Model)JsonConvert.DeserializeObject(error_string, typeof(Error_Model));

                    return StatusCode((int)response.StatusCode, error);
                }

            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }




        [SwaggerOperation(Summary = "Get purchase invoice IDs")]
        [HttpPost("ids")]
        public async Task<IActionResult> GetPurchaseInvoiceIDs([FromHeader(Name = "ApiKey")][Required] string api_key)
        {
            try
            {
                PurchaseInvoiceID result = new PurchaseInvoiceID();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/purchase-invoice/ids");
                request.Headers.Add("ApiKey", api_key);
                request.Headers.Add("Accept", "application/json");
                //request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();

                    result = (PurchaseInvoiceID)JsonConvert.DeserializeObject(response_string, typeof(PurchaseInvoiceID));
                    return Ok(result);
                }
                else
                {
                    string error_string = await response.Content.ReadAsStringAsync();
                    Error_Model error = (Error_Model)JsonConvert.DeserializeObject(error_string, typeof(Error_Model));

                    return StatusCode((int)response.StatusCode, error);
                }

            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }


        [SwaggerOperation(Summary = "Get purchase invoice in json format")]
        [HttpGet("sync")]
        public async Task<IActionResult> GetPurchaseInvoiceInJsonFormat([FromHeader(Name = "ApiKey")][Required] string api_key)
        {
            try
            {
                List<PurchaseInvoiceStatusChangeDto> eventList = await _uow.PurchaseInvoice.GetChangedPurchaseInvoice(api_key);
                List<GroupInvoice> groupInvoice = new List<GroupInvoice>();
                GroupInvoice inv = new GroupInvoice();

                foreach (var item in eventList)
                {
                    GroupInvoice resultJson = await _uow.PurchaseInvoice.JsonFormatPurchaseInvoice(api_key, item);

                    if (resultJson == null)
                    {
                        continue;
                    }
                    else
                    {

                        inv.InvoiceAll = resultJson.InvoiceAll;
                        groupInvoice.Add(resultJson);

                    }

                }

                return Ok(groupInvoice);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }


        [SwaggerOperation(Summary = "Subscribe for the next day to receive invoice status change notifications")]
        [HttpPost("subscribe")]
        public async Task<IActionResult> SubscribeToNextDay([FromHeader(Name = "ApiKey")][Required] string api_key)
        {
            try
            {

                List<SalesInvoiceStatusChangeDto> token = new List<SalesInvoiceStatusChangeDto>();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/subscribe");
                request.Headers.Add("ApiKey", api_key);
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(JsonConvert.SerializeObject(api_key), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    return Ok();

                }
                else
                {
                    string error_string = await response.Content.ReadAsStringAsync();
                    Error_Model error = (Error_Model)JsonConvert.DeserializeObject(error_string, typeof(Error_Model));

                    return StatusCode((int)response.StatusCode, error);

                }
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

        }

        [SwaggerOperation(Summary = "Download purchase invoice ubl as FileStream, archive if more than one")]
        [HttpGet("xml")]
        public async Task<IActionResult> DownloadPurchaseInvoiceUbl([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] string invoiceId)
        {
            try
            {

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/purchase-invoice/xml?invoiceId=" + invoiceId);
                request.Headers.Add("ApiKey", api_key);
                request.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();

                    return Ok(response_string);
                }
                else
                {
                    string error_string = await response.Content.ReadAsStringAsync();
                    Error_Model error = (Error_Model)JsonConvert.DeserializeObject(error_string, typeof(Error_Model));

                    return StatusCode((int)response.StatusCode, error);
                }

            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }


        [SwaggerOperation(Summary = "Get sales invoice in XML file")]
        [HttpGet("XmlSync")]
        public async Task<IActionResult> XmlFormatPurchaseInvoice([FromHeader(Name = "ApiKey")][Required] string api_key)
        {
            try
            {
                XmlFormatInvoice objInvoiceXml = new XmlFormatInvoice();
                List<XmlFormatInvoice> invoiceXml = new List<XmlFormatInvoice>();
                List<PurchaseInvoiceStatusChangeDto> eventList = await _uow.PurchaseInvoice.GetChangedPurchaseInvoice(api_key);

                foreach (var item in eventList)
                {
                    XmlFormatInvoice resultXml = await _uow.PurchaseInvoice.XmlFormatPurchaseInvoice(api_key, item);

                    if (resultXml == null)
                    {
                        continue;
                    }
                    else
                    {
                        invoiceXml.Add(resultXml);
                    }


                }
                return Ok(invoiceXml);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Get invoice document")]
        [HttpGet("ReturnDocumentPdf")]
        public async Task<IActionResult> PurchaseInvoicePdf([FromHeader(Name = "ApiKey")][Required] string api_key)
        {
            try
            {
                List<ReturnEnvDocumentPdf> invoicePdf = new List<ReturnEnvDocumentPdf>();
                List<PurchaseInvoiceStatusChangeDto> eventList = await _uow.PurchaseInvoice.GetChangedPurchaseInvoice(api_key);

                foreach (var item in eventList)
                {
                    ReturnEnvDocumentPdf documentPdf = await _uow.PurchaseInvoice.PurchaseInvoicePdf(api_key, item);
                    invoicePdf.Add(documentPdf);
                }
                return Ok(invoicePdf);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

        }


        [SwaggerOperation(Summary = "Get individual purchase invoice PDF document")]
        [HttpPost("ReturnIndividualPDF")]
        public async Task<IActionResult> ReturnDocumentPdfIndividual([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] string InvoiceId)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/purchase-invoice/xml?invoiceId=" + InvoiceId);
            request.Headers.Add("ApiKey", api_key);
            request.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string response_string = await response.Content.ReadAsStringAsync();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response_string);

                string fromXml = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
                ReturnEnvDocumentPdf fromJson = JsonConvert.DeserializeObject<ReturnEnvDocumentPdf>(fromXml);

                return Ok(fromJson);
            }
            else
            {
                string error_string = await response.Content.ReadAsStringAsync();
                Error_Model error = (Error_Model)JsonConvert.DeserializeObject(error_string, typeof(Error_Model));

                return StatusCode((int)response.StatusCode, error);
            }
        }



    }

}