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
using System.Net.Mail;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;


namespace eInvoice.Controllers
{
    public class SalesInvoiceController : BaseController
    {
        private readonly IUnitOfWork _uow;
        public SalesInvoiceController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [ProducesResponseType(typeof(MiniInvoiceDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Import sales ubl")]
        [HttpPost("ubl")]
        public async Task<IActionResult> ImportSalesUbl([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] sendToCir sendToCir, [FromBody] Request_ImportSalesUbl body)
        {

            try
            {

                CompanyAccountIdentificationDto bodyCheck = new CompanyAccountIdentificationDto();
                MiniInvoiceDto result = new MiniInvoiceDto();
                string ubl_file = _uow.SalesInvoice.FillSalesUbl(body);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/sales-invoice/ubl?requestId=" + body.Id + "&sendToCir=" + sendToCir);
                request.Headers.Add("ApiKey", api_key);
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(@ubl_file, Encoding.UTF8, "application/xml");
                HttpResponseMessage response = await httpClient.SendAsync(request);

                bodyCheck.registrationNumber = body.MaticniBrojKupca;
                bodyCheck.jbkjs = body.IdentifikatorKupca;
                CompanyAccountOnEfAkturaDto identification = await _uow.SalesInvoice.CheckIfCompanyRegisteredOnEfaktura(bodyCheck);

                if (identification.eFakturaRegisteredCompany == true)
                {

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string response_string = await response.Content.ReadAsStringAsync();
                        result = (MiniInvoiceDto)JsonConvert.DeserializeObject(response_string, typeof(MiniInvoiceDto));
                        _uow.BaseRepository.WriteToLogFile(response_string, logFile_path);
                        if (body.EMailAdresaProdavca != "")
                        {
                            await _uow.SalesInvoice.SendEmail(body);
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
                else
                {
                    await _uow.SalesInvoice.SendEmail(body);
                    return BadRequest("Company" + body.MaticniBrojKupca + " is not registered on E-Fakture");

                }
            }
            catch (Exception ex)
            {
                _uow.BaseRepository.WriteToLogFile(ex.Message, logFile_path);
                _uow.BaseRepository.WriteToLogFile(ex.Source, logFile_path);
                _uow.BaseRepository.WriteToLogFile(ex.StackTrace, logFile_path);
                return Conflict(ex.Message);
            }
        }


        [ProducesResponseType(typeof(MiniInvoiceDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Import credit note invoice")]
        [HttpPost("ubl-CreditNote")]
        public async Task<IActionResult> ImportCreditNote([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] sendToCir sendToCir, [FromBody] Request_CreditNote2 body)
        {

            try
            {
                CompanyAccountIdentificationDto bodyCheck = new CompanyAccountIdentificationDto();
                MiniInvoiceDto result = new MiniInvoiceDto();
                string ubl_file = _uow.CreditNote.FillCreditNote(body);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/sales-invoice/ubl?requestId=" + body.Id + "&sendToCir=" + sendToCir);
                request.Headers.Add("ApiKey", api_key);
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(@ubl_file, Encoding.UTF8, "application/xml");
                HttpResponseMessage response = await httpClient.SendAsync(request);

                bodyCheck.registrationNumber = body.MaticniBrojKupca;
                bodyCheck.jbkjs = body.IdentifikatorKupca;
                CompanyAccountOnEfAkturaDto identification = await _uow.CreditNote.CheckIfCompanyRegisteredOnEfaktura(bodyCheck);
                if (identification.eFakturaRegisteredCompany == true)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string response_string = await response.Content.ReadAsStringAsync();
                        result = (MiniInvoiceDto)JsonConvert.DeserializeObject(response_string, typeof(MiniInvoiceDto));
                        _uow.BaseRepository.WriteToLogFile(response_string, logFile_path);
                        if (body.EMailAdresaProdavca != "")
                        {
                            await _uow.CreditNote.SendEmail(body);
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
                else
                {
                    await _uow.CreditNote.SendEmail(body);
                    return BadRequest("Company" + body.MaticniBrojKupca + " is not registered on E-Fakture");
                }
            }
            catch (Exception ex)
            {
                _uow.BaseRepository.WriteToLogFile(ex.Message, logFile_path);
                _uow.BaseRepository.WriteToLogFile(ex.Source, logFile_path);
                _uow.BaseRepository.WriteToLogFile(ex.StackTrace, logFile_path);
                return Conflict(ex.Message);
            }
        }



        [ProducesResponseType(typeof(MiniInvoiceDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Import final invoice")]
        [HttpPost("ubl-FinalInvoice")]
        public async Task<IActionResult> ImportFinalInvoice([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] sendToCir sendToCir, [FromBody] Reques_FinalInvoice body)
        {

            try
            {
                CompanyAccountIdentificationDto bodyCheck = new CompanyAccountIdentificationDto();
                MiniInvoiceDto result = new MiniInvoiceDto();
                string ubl_file = _uow.FinalInvoice.FillFinalInvoice(body);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/sales-invoice/ubl?requestId=" + body.Id + "&sendToCir=" + sendToCir);
                request.Headers.Add("ApiKey", api_key);
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(@ubl_file, Encoding.UTF8, "application/xml");
                HttpResponseMessage response = await httpClient.SendAsync(request);

                bodyCheck.registrationNumber = body.MaticniBrojKupca;
                bodyCheck.jbkjs = body.IdentifikatorKupca;
                CompanyAccountOnEfAkturaDto identification = await _uow.FinalInvoice.CheckIfCompanyRegisteredOnEfaktura(bodyCheck);
                if (identification.eFakturaRegisteredCompany == true)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string response_string = await response.Content.ReadAsStringAsync();

                        result = (MiniInvoiceDto)JsonConvert.DeserializeObject(response_string, typeof(MiniInvoiceDto));
                        _uow.BaseRepository.WriteToLogFile(response_string, logFile_path);
                        if (body.EMailAdresaProdavca != "")
                        {
                            await _uow.FinalInvoice.SendEmail(body);
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
                else
                {
                    await _uow.FinalInvoice.SendEmail(body);
                    return BadRequest("Company" + body.MaticniBrojKupca + " is not registered on E-Fakture");
                }
            }
            catch (Exception ex)
            {
                _uow.BaseRepository.WriteToLogFile(ex.Message, logFile_path);
                _uow.BaseRepository.WriteToLogFile(ex.Source, logFile_path);
                _uow.BaseRepository.WriteToLogFile(ex.StackTrace, logFile_path);
                return Conflict(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Delete only draft or new sales invoices, other invoices cannot be deleted")]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteInvoice([FromHeader(Name = "ApiKey")][Required] string api_key, int invoiceId)
        {
            try
            {

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, server_url + "/api/publicApi/sales-invoice/" + invoiceId);
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

        [SwaggerOperation(Summary = "Delete only draft or new sales invoices, other invoices are ignored")]
        [HttpDelete("deleteInvoiceArray")]
        public async Task<IActionResult> DeleteInvoiceArray([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] int[] invoiceId)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, server_url + "/api/publicApi/sales-invoice?invoiceId=" + invoiceId.ToString());
                request.Headers.Add("ApiKey", api_key);
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(JsonConvert.SerializeObject(api_key), Encoding.UTF8, "application/json");

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


        [ProducesResponseType(typeof(SimpleSalesInvoiceDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Get sales invoice")]
        [HttpGet()]
        public async Task<IActionResult> GetSalesInvoice([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] string invoiceId)
        {
            try
            {
                SimpleSalesInvoiceDto result = new SimpleSalesInvoiceDto();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/sales-invoice?invoiceId=" + invoiceId);
                request.Headers.Add("ApiKey", api_key);
                request.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = (SimpleSalesInvoiceDto)JsonConvert.DeserializeObject(response_string, typeof(SimpleSalesInvoiceDto));

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


        [ProducesResponseType(typeof(InvoiceDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Cancel invoice")]
        [HttpPost("cancel")]
        public async Task<IActionResult> Cancelinvoice([FromHeader(Name = "ApiKey")][Required] string api_key, [FromBody] CancelInvoiceMessageDto body)
        {
            try
            {
                InvoiceDto result = new InvoiceDto();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/sales-invoice/cancel");
                request.Headers.Add("ApiKey", api_key);
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<InvoiceDto>(response_string);
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


        [ProducesResponseType(typeof(InvoiceDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Invoice Cancellation")]
        [HttpPost("storno")]
        public async Task<ActionResult> InvoiceCancellation([FromHeader(Name = "ApiKey")][Required] string api_key, [FromBody] StornoInvoiceMessageDto body)
        {
            try
            {
                InvoiceDto result = new InvoiceDto();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/sales-invoice/storno");
                request.Headers.Add("apikey", api_key);
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<InvoiceDto>(response_string);
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


        [SwaggerOperation(Summary = "Download sales invoice ubl as FileStream, archive if more than one")]
        [HttpGet("xml")]
        public async Task<IActionResult> DownloadSalesInvoiceUbl([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] string invoiceId)
        {
            try
            {

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/sales-invoice/xml?invoiceId=" + invoiceId);
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

        [ProducesResponseType(typeof(List<SalesInvoiceStatusChangeDto>), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Search for all sales invoices which status changed on specific date")]
        [HttpPost("changes")]
        public async Task<ActionResult> SearchAllSalesInvoicesWhichStatusChangedOnSpecificDate([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] DateTime date)
        {
            try
            {
                List<SalesInvoiceStatusChangeDto> result = new List<SalesInvoiceStatusChangeDto>();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/sales-invoice/changes?date=" + date);
                request.Headers.Add("apikey", api_key);
                request.Headers.Add("Accept", "appliaction/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    if (response_string != "[]")
                    {
                        result = JsonConvert.DeserializeObject<List<SalesInvoiceStatusChangeDto>>(response_string);
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
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

        }



        [ProducesResponseType(typeof(List<SalesInvoiceStatusChangeDto>), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Search for salse invoice witch status change on last day")]
        [HttpPost("lastday")]
        public async Task<ActionResult> SalesInvoiceStatusLastDay(string api_key)
        {
            try
            {

                var result = await _uow.SalesInvoice.SalesInvoiceStatusLastDay(api_key);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [ProducesResponseType(typeof(List<ValueAddedTaxExemptionReasonDto>), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Get list of all exemption reasons")]
        [HttpGet("getValueAddedTaxExemptionReasonList")]
        public async Task<ActionResult> GetListOfAllExemptionReasons([FromHeader(Name = "ApiKey")][Required] string api_key)
        {
            try
            {
                List<ValueAddedTaxExemptionReasonDto> result = new List<ValueAddedTaxExemptionReasonDto>();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/sales-invoice/getValueAddedTaxExemptionReasonList");
                request.Headers.Add("apikey", api_key);
                request.Headers.Add("Accept", "appliaction/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = (List<ValueAddedTaxExemptionReasonDto>)Newtonsoft.Json.JsonConvert.DeserializeObject(response_string, typeof(List<ValueAddedTaxExemptionReasonDto>));

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

        [ProducesResponseType(typeof(SearchInvoiceResultDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Search for CIR invoices")]
        [HttpPost("findCirInvoices")]
        public async Task<IActionResult> SearchCIRInvoices([FromHeader(Name = "ApiKey")][Required] string api_key, [FromBody] SearchParameter body)
        {
            try
            {
                SearchInvoiceResultDto result = new SearchInvoiceResultDto();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/sales-invoice/findCirInvoices");
                request.Headers.Add("apikey", api_key);
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");


                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<SearchInvoiceResultDto>(response_string);
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

        [SwaggerOperation(Summary = "Get sales invoice in XML file")]
        [HttpGet("XmlSync")]
        public async Task<IActionResult> XmlFormatSalesInvoice([FromHeader(Name = "ApiKey")][Required] string api_key)
        {
            try
            {
                List<XmlFormatInvoice> XmlInvoice = new List<XmlFormatInvoice>();
                XmlFormatInvoice objXmlInvoice = new XmlFormatInvoice();
                List<SalesInvoiceStatusChangeDto> eventList = await _uow.SalesInvoice.GetChangedSalesInvoice(api_key);

                foreach (var item in eventList)
                {
                    XmlFormatInvoice resultXml = await _uow.SalesInvoice.XmlFormatSalesInvoice(api_key, item);

                    if (resultXml == null)
                    {
                        continue;
                    }
                    else
                    {

                        objXmlInvoice.InvoiceXML = resultXml.InvoiceXML;
                        XmlInvoice.Add(resultXml);
                    }
                }

                return Ok(XmlInvoice);

            }

            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

        }

        [SwaggerOperation(Summary = "Get invoice document")]
        [HttpGet("ReturnDocumentPdf")]
        public async Task<IActionResult> InvoiceDocumentPdf([FromHeader(Name = "ApiKey")][Required] string api_key)
        {
            try
            {

                List<ReturnEnvDocumentPdf> invoicePdf = new List<ReturnEnvDocumentPdf>();
                List<SalesInvoiceStatusChangeDto> eventList = await _uow.SalesInvoice.GetChangedSalesInvoice(api_key);

                foreach (var item in eventList)
                {

                    ReturnEnvDocumentPdf invoiceDocumentPdf1 = await _uow.SalesInvoice.InvoiceDocumentPdf(api_key, item);
                    invoicePdf.Add(invoiceDocumentPdf1);

                }

                return Ok(invoicePdf);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }


        [SwaggerOperation(Summary = "Get individual sales invoice PDF document")]
        [HttpPost("ReturnIndividualPDF")]
        public async Task<IActionResult> ReturnDocumentPdfIndividual([FromHeader(Name = "ApiKey")][Required] string api_key, [Required] string InvoiceId)
        {

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, server_url + "/api/publicApi/sales-invoice/xml?invoiceId=" + InvoiceId);
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


        [SwaggerOperation(Summary = "Get sales invoice in json format")]
        [HttpGet("sync")]
        public async Task<IActionResult> GetSalesInvoiceInJsonFormat([FromHeader(Name = "ApiKey")][Required] string api_key)
        {
            try
            {

                List<GroupInvoice> groupInvoice = new List<GroupInvoice>();
                List<SalesInvoiceStatusChangeDto> eventList = await _uow.SalesInvoice.GetChangedSalesInvoice(api_key);
                GroupInvoice inv = new GroupInvoice();

                foreach (var item in eventList)
                {
                    GroupInvoice resultJson = await _uow.SalesInvoice.JsonFormatSalesInvoice(api_key, item);

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


        [SwaggerOperation(Summary = "Subscribe for the next day to receive invoice status change notifications ")]
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


        [SwaggerOperation(Summary = "Get sales invoice IDs")]
        [HttpPost("ids")]
        public async Task<IActionResult> GetSalesInvoiceIDs([FromHeader(Name = "ApiKey")][Required] string api_key)
        {
            try
            {
                SalesInvoicesDto result = new SalesInvoicesDto();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, server_url + "/api/publicApi/sales-invoice/ids");
                request.Headers.Add("ApiKey", api_key);
                request.Headers.Add("Accept", "application/json");
                //request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");


                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string response_string = await response.Content.ReadAsStringAsync();

                    result = (SalesInvoicesDto)JsonConvert.DeserializeObject(response_string, typeof(SalesInvoicesDto));
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
