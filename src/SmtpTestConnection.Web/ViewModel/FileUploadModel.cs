using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmtpTestConnection.Web.ViewModel
{
    public class FileUploadModel
    {
        public IFormFile Htmlfile { get; set; }
        [Display( Name = "Enter API Key")]
        public string ApiKey { get; set; }
        [Display(Name = "Name Your Template")]
        public string TemplateName { get; set; }
    }
}
