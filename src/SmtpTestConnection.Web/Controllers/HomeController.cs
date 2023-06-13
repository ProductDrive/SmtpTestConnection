using Microsoft.AspNetCore.Mvc;

namespace SmtpTestConnection.Web.Controllers
{
    public class HomeController : SmtpTestConnectionControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
               
        public ActionResult Documentation()
        {
            return View();
        }
    }
}