using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmtpTestConnection.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SmtpTestConnection.Web.Controllers
{
    public class TemplateController : SmtpTestConnectionControllerBase
    {
        public async Task<ActionResult> Index()
        {
            HttpClient client = InitializeHttpClient();
            string url = $"https://cdacollections.projectdriveng.com.ng/api/Job/templategal";
            //string url = $"https://localhost:44378/api/job/templategal";
            ResponseModel result;
            HttpResponseMessage httpResponse = await client.GetAsync(url);
            if (httpResponse.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<ResponseModel>(await httpResponse.Content.ReadAsStringAsync());
                //string rawMsg = result.ReturnObj.ToString();
                return View(result);
            }
            return View(new ResponseModel());
        }

        
        public async Task<ActionResult> CustomeTemplate(ResponseModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.RequestApi))
            {
                if (model.RequestApi == "Enter API Key")
                {
                    return View(new ResponseModel() { Errors = new List<string> { "Enter API Key" } });
                }

                if (!string.IsNullOrWhiteSpace(model.RequestApi) && model.Response == "getkey")
                {
                    var key = "PrD" + Guid.NewGuid().ToString().Substring(2, 3).ToLower() + DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + Guid.NewGuid().ToString().Substring(9, 3);
                    return View(new ResponseModel { Response = "", Errors = new List<string> { key } });
                }

                if (!string.IsNullOrWhiteSpace(model.RequestApi) && model.Response != "getkey")
                {
                    HttpClient client = InitializeHttpClient();
                    string url = $"https://cdacollections.projectdriveng.com.ng/api/Job/customtemplates?apiKey={model.RequestApi}";
                    //string url = $"https://localhost:44378/api/job/customtemplates?apiKey={model.Response}";
                    ResponseModel result;
                    HttpResponseMessage httpResponse = await client.GetAsync(url);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        result = JsonConvert.DeserializeObject<ResponseModel>(await httpResponse.Content.ReadAsStringAsync());
                        //string rawMsg = result.ReturnObj.ToString();
                        return View(result);
                    }
                }
            }
            return View(new ResponseModel());
        }

        public ActionResult GenerateTemplateAPIKey()
        {
              //var key = "PrD" + Guid.NewGuid().ToString().Substring(2, 3).ToLower() + DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + Guid.NewGuid().ToString().Substring(9, 3).ToUpper();
                return RedirectToAction("UploadTemplate", new { apikey = "" });
        }

        [HttpGet]
        public ActionResult UploadTemplate(string apikey)
        {
            if (string.IsNullOrWhiteSpace(apikey))
            {
                var key = "PrD" + Guid.NewGuid().ToString().Substring(2, 3).ToLower() + DateTimeOffset.Now.ToUnixTimeSeconds().ToString().Substring(0,10) + Guid.NewGuid().ToString().Substring(9, 3).ToUpper();
                return View(new FileUploadModel {ApiKey = key});
            }
            return View(new FileUploadModel { ApiKey = apikey });
        }


        [HttpPost]
        public async Task<ActionResult> UploadTemplate(FileUploadModel model)
        {
            if (string.IsNullOrWhiteSpace(model.ApiKey) || string.IsNullOrWhiteSpace(model.TemplateName) || model.Htmlfile.Length < 1)
            {
                return View(new FileUploadModel { ApiKey = "Invalid entries: All fields must be filled and file must be chosen" });
            }

            HttpClient client = InitializeHttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            var form = new MultipartFormDataContent();

            using (var ms = new MemoryStream())
            {
                await model.Htmlfile.CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);
                var content = new StreamContent(ms);
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "Htmlfile",
                    FileName = model.Htmlfile.FileName
                };
                form.Add(content, "Htmlfile");
                client.DefaultRequestHeaders.Add("ApiKey", $"{model.ApiKey}");
                client.DefaultRequestHeaders.Add("TemplateName", $"{model.TemplateName}");

                string url = $"https://cdacollections.projectdriveng.com.ng/api/Job/uploadtemp";
                //string url = $"https://localhost:44378/api/Job/uploadtemp";

                try
                {
                    var postResponse = await client.PostAsync(url, form);
                    if (postResponse.IsSuccessStatusCode)
                    {
                        var res = await postResponse.Content.ReadAsStringAsync();

                        if (res == "true")
                        {
                            return RedirectToAction("CustomeTemplate", new ResponseModel { Response = model.ApiKey });
                        }
                    }


                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            return RedirectToAction("Index");
        }


        public ActionResult DisplayTemplate(ResponseModel model)
        {
            return View(model.Response);
        }

        private static HttpClient InitializeHttpClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://cdacollections.projectdriveng.com.ng/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

    }

    
}
