using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SettingsService.Api.Tests.Fixtures;
using SettingsService.Core.Data.Models;
using Xunit;

namespace SettingsService.Api.Tests.Controllers
{
    public class ApiHostsTests : IClassFixture<HttpServerFixture>, IClassFixture<TestDbFixture>
    {
        private readonly HttpServerFixture _httpServer;
        private readonly TestDbFixture _testDb;

        public ApiHostsTests(HttpServerFixture httpServer, TestDbFixture testDb)
        {
            _httpServer = httpServer;
            _testDb = testDb;
        }

        [Fact(DisplayName = "api/hosts: should return host settings by id")]
        public void Should_return_default_host_settings()
        {
            using (var ctx = _testDb.CreateContext())
            {
                ctx.CrawlHostSettings.AddRange(new[]
                {
                    new CrawlHostSetting {CrawlDelay = 1, Disallow = "*", Host = string.Empty}
                });
                ctx.SaveChanges();
                // get ID for request
                var id = ctx.CrawlHostSettings.ToList().First().Id.ToString();

                using (var response = _httpServer.GetJson("api/hosts/"+ id))
                {
                    var content = response.Content as ObjectContent<CrawlHostSetting>;
                    Assert.NotNull(content);

                    var result = content.Value as CrawlHostSetting;
                    Assert.NotNull(result);

                    Assert.Equal(1, result.CrawlDelay);
                    Assert.Equal("*", result.Disallow);
                    Assert.Equal("", result.Host);
                }
            }
        }

        [Fact(DisplayName = "api/hosts: should return the list of available hosts")]
        public void Should_return_the_list_of_available_hosts()
        {
            using (var ctx = _testDb.CreateContext())
            {
                ctx.CrawlHostSettings.AddRange(new[]
                {
                    new CrawlHostSetting {CrawlDelay = 1, Disallow = "*", Host = "", Id = Guid.NewGuid()},
                    new CrawlHostSetting {CrawlDelay = 2, Disallow = "*", Host = "1", Id = Guid.NewGuid()},
                    new CrawlHostSetting {CrawlDelay = 3, Disallow = "*", Host = "2", Id = Guid.NewGuid()},
                });
                ctx.SaveChanges();

                using (var response = _httpServer.GetJson("api/hosts"))
                {
                    var content = response.Content as ObjectContent<IList<CrawlHostSetting>>;
                    Assert.NotNull(content);

                    var result = content.Value as IList<CrawlHostSetting>;
                    Assert.NotNull(result);

                    Assert.Equal(3, result.Count);
                }
            }
        }
    }
}