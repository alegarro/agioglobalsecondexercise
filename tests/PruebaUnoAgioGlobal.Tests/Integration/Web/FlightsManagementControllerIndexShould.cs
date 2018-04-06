using PruebaUnoAgioGlobal.Tests.Integration.WebBase;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PruebaUnoAgioGlobal.Tests.Integration.Web
{
    public class FlightsManagementControllerIndexShould : BaseWebTest
    {
        public FlightsManagementControllerIndexShould()
        {
            // Open Bug in Automapper dependency injection (https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection/issues/36) when run test in paralell, random sleep is a workaround that I did and it works!
            Thread.Sleep(new Random(DateTime.Now.Millisecond).Next(5000));
        }

        [Fact]
        public async Task ReturnIndexPage()
        {
            var response = await Client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Contains("Prueba Agio Global", stringResponse);
        }

        [Fact]
        public async Task IndexHasProperlyData()
        {
            var response = await Client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            Assert.Contains("IB 4567", stringResponse);
            Assert.Contains("Iberia", stringResponse);
            Assert.Contains("FR 7298", stringResponse);
            Assert.Contains("Ryanair", stringResponse);
        }
    }
}