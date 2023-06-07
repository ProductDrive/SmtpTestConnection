using Abp.AspNetCore.Mvc.Views;

namespace SmtpTestConnection.Web.Views
{
    public abstract class SmtpTestConnectionRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected SmtpTestConnectionRazorPage()
        {
            LocalizationSourceName = SmtpTestConnectionConsts.LocalizationSourceName;
        }
    }
}
