using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SmtpTestConnection.Configuration;
using SmtpTestConnection.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;

namespace SmtpTestConnection.Web.Startup
{
    [DependsOn(
        typeof(SmtpTestConnectionApplicationModule), 
        typeof(SmtpTestConnectionEntityFrameworkCoreModule), 
        typeof(AbpAspNetCoreModule))]
    public class SmtpTestConnectionWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public SmtpTestConnectionWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(SmtpTestConnectionConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<SmtpTestConnectionNavigationProvider>();

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(SmtpTestConnectionApplicationModule).GetAssembly()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SmtpTestConnectionWebModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(SmtpTestConnectionWebModule).Assembly);
        }
    }
}