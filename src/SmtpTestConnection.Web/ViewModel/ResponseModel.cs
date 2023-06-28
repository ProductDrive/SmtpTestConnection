using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmtpTestConnection.Web.ViewModel
{
    public class ResponseModel
    {
        public string Response { get; set; }
        public bool Status { get; set; }
        public object ReturnObj { get; set; }
        public Dictionary<string, string> Templates => ReturnObj == null? null: JsonConvert.DeserializeObject<Dictionary<string, string>>(ReturnObj.ToString());
        public List<string> Errors { get; set; }
        public string RequestApi { get; set; }

    }

    public class TemplateModel
    {

    }
}
