using Abp.Application.Services;

namespace SmtpTestConnection
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class SmtpTestConnectionAppServiceBase : ApplicationService
    {
        protected SmtpTestConnectionAppServiceBase()
        {
            LocalizationSourceName = SmtpTestConnectionConsts.LocalizationSourceName;
        }
    }
}