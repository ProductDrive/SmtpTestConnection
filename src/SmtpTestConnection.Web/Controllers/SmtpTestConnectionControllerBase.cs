using Abp.AspNetCore.Mvc.Controllers;

namespace SmtpTestConnection.Web.Controllers
{
    public abstract class SmtpTestConnectionControllerBase: AbpController
    {
        protected SmtpTestConnectionControllerBase()
        {
            LocalizationSourceName = SmtpTestConnectionConsts.LocalizationSourceName;
        }
    }
}