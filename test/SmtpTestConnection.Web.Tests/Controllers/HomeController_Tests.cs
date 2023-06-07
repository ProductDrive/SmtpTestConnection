using System.Threading.Tasks;
using SmtpTestConnection.Web.Controllers;
using Shouldly;
using Xunit;

namespace SmtpTestConnection.Web.Tests.Controllers
{
    public class HomeController_Tests: SmtpTestConnectionWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
