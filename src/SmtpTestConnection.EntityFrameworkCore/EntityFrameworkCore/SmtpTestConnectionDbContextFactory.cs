using SmtpTestConnection.Configuration;
using SmtpTestConnection.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SmtpTestConnection.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class SmtpTestConnectionDbContextFactory : IDesignTimeDbContextFactory<SmtpTestConnectionDbContext>
    {
        public SmtpTestConnectionDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SmtpTestConnectionDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(SmtpTestConnectionConsts.ConnectionStringName)
            );

            return new SmtpTestConnectionDbContext(builder.Options);
        }
    }
}