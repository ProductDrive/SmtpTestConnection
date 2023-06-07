using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SmtpTestConnection.Web.Startup;
namespace SmtpTestConnection.Web.Tests
{
    [DependsOn(
        typeof(SmtpTestConnectionWebModule),
        typeof(AbpAspNetCoreTestBaseModule)
        )]
    public class SmtpTestConnectionWebTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SmtpTestConnectionWebTestModule).GetAssembly());
        }
    }
}