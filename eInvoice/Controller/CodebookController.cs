using eInvoice.Controllers.api;
using eInvoice.Core.Models;
using eInvoice.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace eInvoice.Controllers
{
    public class CodebookController : BaseController
    {
        private readonly IUnitOfWork _uow;
        public CodebookController(IUnitOfWork uow)
        {
            _uow = uow;
        }


        [HttpGet("enviroment")]
        public IActionResult TestApi()
        {

            List<KeyValue> list = new List<KeyValue>();
            KeyValue item = new KeyValue();
            item.Key = "server";
            item.Value = server_url;
            list.Add(item);

            item = new KeyValue();
            item.Key = "log";
            item.Value = logFile_path;
            list.Add(item);

            item = new KeyValue();
            item.Key = "template";
            item.Value = ubl_storage;
            list.Add(item);

            item = new KeyValue();
            item.Key = "build time";
            DateTime buildDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
            item.Value = buildDate.ToString();
            list.Add(item);

            return Ok(list);
        }

        [ProducesResponseType(typeof(List<KeyValue>), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Get Invoice Type Code")]
        [HttpGet("InvoiceTypeCode")]
        public IActionResult GetInvoiceTypeCode()
        {

            try
            {
                List<KeyValue> list = new List<KeyValue>();
                KeyValue item = new KeyValue();
                item.Key = "380";
                item.Value = "Komercijalna faktura";
                list.Add(item);

                item = new KeyValue();
                item.Key = "381";
                item.Value = "Knjižno odobrenje";
                list.Add(item);

                item = new KeyValue();
                item.Key = "383";
                item.Value = "Knjižno zaduženje";
                list.Add(item);

                item = new KeyValue();
                item.Key = "384";
                item.Value = "Korigovana faktura";
                list.Add(item);

                item = new KeyValue();
                item.Key = "386";
                item.Value = "Avansna faktura";
                list.Add(item);


                return Ok(list);
            }
            catch (Exception ex)
            {

                return Conflict(ex.Message);
            }

        }

        [ProducesResponseType(typeof(List<KeyValue>), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Get Vat Category Code")]
        [HttpGet("VATCategoryCode")]
        public IActionResult GetVATCategoryCode()
        {

            try
            {
                List<KeyValue> list = new List<KeyValue>();
                KeyValue item = new KeyValue();
                item.Key = "S";
                item.Value = "Standardna stopa";
                list.Add(item);

                item = new KeyValue();
                item.Key = "Z";
                item.Value = "Nulta stopa";
                list.Add(item);

                item = new KeyValue();
                item.Key = "E";
                item.Value = "Izuzeto od PDV-a";
                list.Add(item);

                item = new KeyValue();
                item.Key = "AE";
                item.Value = "Obrnuta naplata PDV-a";
                list.Add(item);

                item = new KeyValue();
                item.Key = "O";
                item.Value = "Usluga van okvira primene PDV-a";
                list.Add(item);

                return Ok(list);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

        }

        [ProducesResponseType(typeof(CompanyAccountOnEfAkturaDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Check if company has eFaktura account")]
        [HttpPost("CheckIfCompanyRegisteredOnEfaktura")]
        public async Task<IActionResult> CheckIfCompanyRegisteredOnEfaktura([FromBody] CompanyAccountIdentificationDto body)
        {
            try
            {
                CompanyAccountOnEfAkturaDto result = new CompanyAccountOnEfAkturaDto();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/Company/CheckIfCompanyRegisteredOnEfaktura");
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");


                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<CompanyAccountOnEfAkturaDto>(response_string);
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

        [SwaggerOperation(Summary = "Get unit of measures")]
        [HttpGet("get-unit-measures")]
        public async Task<IActionResult> GetUnitOfMeasures([FromHeader(Name = "ApiKey")] string api_key)
        {
            try
            {

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/get-unit-measures");
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



    }
}