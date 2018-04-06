using Newtonsoft.Json;
using PruebaUnoAgioGlobal.Tests.Integration.WebBase;
using PruebaUnoAgioGlobal.Web.ModelsApi.Airports.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PruebaUnoAgioGlobal.Tests.Integration.WebApi
{
    public class ApiAirportsControllerListShould : BaseWebTest
    {
        public ApiAirportsControllerListShould()
        {
            // Open Bug in Automapper dependency injection (https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection/issues/36) when run test in paralell, random sleep is a workaround that I did and it works!
            Thread.Sleep(new Random(DateTime.Now.Millisecond).Next(5000));
        }

        [Fact]
        public async Task ReturnsItems()
        {
            var response = await Client.GetAsync("/api/airports");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<AirportDTO>>(stringResponse).ToList();

            Assert.Equal(5, result.Count());
            Assert.Equal(1, result.Count(a => a.CodeAndName == "AGP - Málaga - Costa del Sol"));
            Assert.Equal(1, result.Count(a => a.CodeAndName == "MAD - Madrid - Barajas Adolfo Suárez"));
            Assert.Equal(1, result.Count(a => a.CodeAndName == "CDG - París - Charles de Gaulle"));
            Assert.Equal(1, result.Count(a => a.CodeAndName == "AMS - Amsterdam - Schiphol"));
            Assert.Equal(1, result.Count(a => a.CodeAndName == "CRL - Brussels - Charleroi"));            
        }
    }
}