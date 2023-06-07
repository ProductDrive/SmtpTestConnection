using SmtpTestConnection.EntityFrameworkCore;

namespace SmtpTestConnection.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly SmtpTestConnectionDbContext _context;

        public TestDataBuilder(SmtpTestConnectionDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            //create test data here...
        }
    }
}