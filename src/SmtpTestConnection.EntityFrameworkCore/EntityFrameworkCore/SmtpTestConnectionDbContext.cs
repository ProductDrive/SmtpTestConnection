using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SmtpTestConnection.EntityFrameworkCore
{
    public class SmtpTestConnectionDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...

        public SmtpTestConnectionDbContext(DbContextOptions<SmtpTestConnectionDbContext> options) 
            : base(options)
        {

        }
    }
}
