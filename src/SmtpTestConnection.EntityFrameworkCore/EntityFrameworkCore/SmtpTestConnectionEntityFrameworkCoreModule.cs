using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace SmtpTestConnection.EntityFrameworkCore
{
    [DependsOn(
        typeof(SmtpTestConnectionCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class SmtpTestConnectionEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SmtpTestConnectionEntityFrameworkCoreModule).GetAssembly());
        }
    }
}