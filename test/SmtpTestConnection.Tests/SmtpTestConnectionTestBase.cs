using System;
using System.Threading.Tasks;
using Abp.TestBase;
using SmtpTestConnection.EntityFrameworkCore;
using SmtpTestConnection.Tests.TestDatas;

namespace SmtpTestConnection.Tests
{
    public class SmtpTestConnectionTestBase : AbpIntegratedTestBase<SmtpTestConnectionTestModule>
    {
        public SmtpTestConnectionTestBase()
        {
            UsingDbContext(context => new TestDataBuilder(context).Build());
        }

        protected virtual void UsingDbContext(Action<SmtpTestConnectionDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<SmtpTestConnectionDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected virtual T UsingDbContext<T>(Func<SmtpTestConnectionDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<SmtpTestConnectionDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected virtual async Task UsingDbContextAsync(Func<SmtpTestConnectionDbContext, Task> action)
        {
            using (var context = LocalIocManager.Resolve<SmtpTestConnectionDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected virtual async Task<T> UsingDbContextAsync<T>(Func<SmtpTestConnectionDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<SmtpTestConnectionDbContext>())
            {
                result = await func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}
