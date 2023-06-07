using Abp.Modules;
using Abp.Reflection.Extensions;
using SmtpTestConnection.Localization;

namespace SmtpTestConnection
{
    public class SmtpTestConnectionCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            SmtpTestConnectionLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SmtpTestConnectionCoreModule).GetAssembly());
        }
    }
}