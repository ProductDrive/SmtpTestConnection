using Microsoft.EntityFrameworkCore;

namespace SmtpTestConnection.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<SmtpTestConnectionDbContext> dbContextOptions, 
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for SmtpTestConnectionDbContext */
            dbContextOptions.UseSqlServer(connectionString);
        }
    }
}
