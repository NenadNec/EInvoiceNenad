using eInvoice.Controllers.api;
using eInvoice.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Controllers
{
    public class TicketCIRController : BaseController
    {

        [ProducesResponseType(typeof(List<CirTicketListResponse>), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Gets all tickets by CIR invoice Id on from CIR")]
        [HttpGet("{cirInvoiceId}/{onlyActiveTickets}")]
        public async Task<IActionResult> GetAllTicketsByCIRInvoiceId([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] string cirInvoiceId, [Required] bool onlyActiveTickets)
        {
            try
            {
                List<CirTicketListResponse> result = new List<CirTicketListResponse>();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/cir-tickets/" + cirInvoiceId + "/" + onlyActiveTickets);
                request.Headers.Add("ApiKey", api_key);
                request.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = (List<CirTicketListResponse>)JsonConvert.DeserializeObject(response_string, typeof(List<CirTicketListResponse>));

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


        [ProducesResponseType(typeof(CirTicketListResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Gets all tickets from CIR")]
        [HttpPost("find")]
        public async Task<IActionResult> GetsAllTicketsFromCIR([FromHeader(Name = "ApiKey")][Required] string api_key)
        {
            try
            {
                CirTicketListResponse result = new CirTicketListResponse();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/cir-tickets/find");
                request.Headers.Add("ApiKey", api_key);
                request.Headers.Add("Accept", "application/json");


                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<CirTicketListResponse>(response_string);
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
        [SwaggerOperation(Summary = "Creates new ticket on CIR")]
        [HttpPost("addCirTicket")]
        public async Task<IActionResult> CreateNewTicket([FromHeader(Name = "ApiKey")][Required] string api_key, [FromBody] AddCirTicketRequest body)
        {
            try
            {
                int result = 0;


                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/cir-tickets/addCirTicket");
                request.Headers.Add("apikey", api_key);
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");


                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<int>(response_string);
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


        [ProducesResponseType(typeof(CirTicketHistoryDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Get cir ticket history by CIR ticket Id")]
        [HttpGet("getCirTicketHistory/{cirTicketId}")]
        public async Task<IActionResult> GetCIRTicketHistory([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] string cirTicketId)
        {
            try
            {
                CirTicketHistoryDto result = new CirTicketHistoryDto();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/cir-tickets/getCirTicketHistory/" + cirTicketId);
                request.Headers.Add("apikey", api_key);
                request.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = (CirTicketHistoryDto)JsonConvert.DeserializeObject(response_string, typeof(CirTicketHistoryDto));

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


        [ProducesResponseType(typeof(InvoiceHistoryDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Get sales invoice assignation history by CIR invoice Id")]
        [HttpGet("getSalesInvoiceAssignationHistory/{cirInvoiceId}")]
        public async Task<IActionResult> GetSalseInvoiceAssignationByCIRInvoiceID([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] string cirInvoiceId)
        {
            try
            {
                InvoiceHistoryDto result = new InvoiceHistoryDto();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/sales-cir-invoice/getSalesInvoiceAssignationHistory/" + cirInvoiceId);
                request.Headers.Add("apikey", api_key);
                request.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = (InvoiceHistoryDto)JsonConvert.DeserializeObject(response_string, typeof(InvoiceHistoryDto));

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

        [ProducesResponseType(typeof(CirTicketHistoryDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Get invoice payments and history from sales side by CIR invoice Id")]
        [HttpGet("sales-cir-invoice/getInvoicePaymentsAndHistory/{cirInvoiceId}")]
        public async Task<IActionResult> GetInvoicePaymentsAndHistory([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] string cirInvoiceId)
        {
            try
            {
                CirTicketHistoryDto result = new CirTicketHistoryDto();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/sales-cir-invoice/getInvoicePaymentsAndHistory/" + cirInvoiceId);
                request.Headers.Add("apikey", api_key);
                request.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = (CirTicketHistoryDto)JsonConvert.DeserializeObject(response_string, typeof(CirTicketHistoryDto));

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


        [ProducesResponseType(typeof(InvoiceHistoryDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Get purchase invoice assignation history by CIR invoice Id")]
        [HttpGet("getPurchaseInvoiceAssignationHistory/{cirInvoiceId}")]
        public async Task<IActionResult> GetPurchaseInvoiceAssignationHistory([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] string cirInvoiceId)
        {
            try
            {
                InvoiceHistoryDto result = new InvoiceHistoryDto();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/purchase-cir-invoice/getPurchaseInvoiceAssignationHistory/" + cirInvoiceId);
                request.Headers.Add("apikey", api_key);
                request.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = (InvoiceHistoryDto)JsonConvert.DeserializeObject(response_string, typeof(InvoiceHistoryDto));

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


        [ProducesResponseType(typeof(CirHistoryDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Get invoice payment and history from purchase side by CIR invoice id")]
        [HttpGet("purchase-cir-invoice/getInvoicePaymentsAndHistory/{cirInvoiceId}")]
        public async Task<IActionResult> GetInvoicePaymentAndHistoryFormPurchase([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] string cirInvoiceId)
        {
            try
            {
                CirHistoryDto result = new CirHistoryDto();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/purchase-cir-invoice/getInvoicePaymentsAndHistory/" + cirInvoiceId);
                request.Headers.Add("apikey", api_key);
                request.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = (CirHistoryDto)JsonConvert.DeserializeObject(response_string, typeof(CirHistoryDto));

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

    }

}