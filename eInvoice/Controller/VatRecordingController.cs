using eInvoice.Controllers.api;
using eInvoice.Core.Models;
using eInvoice.Core.Models.ERP;
using eInvoice.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;

namespace eInvoice.Controllers
{
    public class VatRecordingController : BaseController
    {
        [SwaggerOperation(Summary = "Summary VAT records")]
        [HttpPost("group")]
        public async Task<IActionResult> ImportGroupVatRecording([FromHeader(Name = "ApiKey")][Required] string api_key, [FromBody] GroupVatDto groupVat)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/vat-recording/group");
                request.Headers.Add("ApiKey", api_key);
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(JsonConvert.SerializeObject(groupVat), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    GroupVatDto result = JsonConvert.DeserializeObject<GroupVatDto>(response_string);
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


        [SwaggerOperation(Summary = "Get all group VAT records")]
        [HttpGet("getAllGroupVatRecording")]
        public async Task<IActionResult> GetAllGroupVatRecording([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] string dateFrom, [Required] string dateTo)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/vat-recording/group?dateFrom=" + dateFrom + "&dateTo=" + dateTo);
                request.Headers.Add("ApiKey", api_key);
                request.Headers.Add("Accept", "application/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    List<GroupVatListDto> result = JsonConvert.DeserializeObject<List<GroupVatListDto>>(response_string);
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


        [SwaggerOperation(Summary = "Downloading certain summary records of VAT")]
        [HttpGet("vat-recording/group/{groupVatId}")]
        public async Task<IActionResult> GetIndividualGroupVatRecording([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] int groupVatId)
        {
            try
            {

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/vat-recording/group/" + groupVatId);
                request.Headers.Add("ApiKey", api_key);
                request.Headers.Add("Accept", "application/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    GroupVatDto result = JsonConvert.DeserializeObject<GroupVatDto>(response_string);
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

        [SwaggerOperation(Summary = "Individaul VAT records")]
        [HttpPost("individual")]
        public async Task<IActionResult> ImportIndividualVatRecording([FromHeader(Name = "ApiKey")][Required] string api_key, [FromBody] IndividualVatDto groupVat)
        {
            try
            {

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/vat-recording/individual");
                request.Headers.Add("ApiKey", api_key);
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(JsonConvert.SerializeObject(groupVat), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    IndividualVatDto result = JsonConvert.DeserializeObject<IndividualVatDto>(response_string);
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


        [SwaggerOperation(Summary = "Get individual VAT record")]
        [HttpGet("vat-recording/individual")]
        public async Task<IActionResult> GetIndividualVatRecording([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] string dateFrom, [Required] string dateTo)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/vat-recording/individual?dateFrom=" + dateFrom + "&dateTo=" + dateTo);
                request.Headers.Add("ApiKey", api_key);
                request.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    List<IndividualVatListDto> result = JsonConvert.DeserializeObject<List<IndividualVatListDto>>(response_string);
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


        [SwaggerOperation(Summary = "This request will return specific individual VAT Record")]
        [HttpGet("vat-recording/individual/{individualVatId}")]
        public async Task<IActionResult> ReturnSpecificVatRecord([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] int individualVatId)
        {
            try
            {

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/vat-recording/individual/" + individualVatId);
                request.Headers.Add("ApiKey", api_key);
                request.Headers.Add("Accept", "application/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    IndividualVatDto result = JsonConvert.DeserializeObject<IndividualVatDto>(response_string);
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