using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmtpTestConnection.Web.ViewModel
{
    public class ConnectionModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public string Port { get; set; }
        public bool IsAuth { get; set; }
    }
}
