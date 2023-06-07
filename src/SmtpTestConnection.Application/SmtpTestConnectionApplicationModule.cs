using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace SmtpTestConnection
{
    [DependsOn(
        typeof(SmtpTestConnectionCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class SmtpTestConnectionApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SmtpTestConnectionApplicationModule).GetAssembly());
        }
    }
}