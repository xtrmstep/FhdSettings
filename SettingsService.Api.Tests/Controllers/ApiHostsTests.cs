using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using SettingsService.Api.Models;
using SettingsService.Api.Tests.Fixtures;
using SettingsService.Core.Data.Models;
using Xunit;

namespace SettingsService.Api.Tests.Controllers
{
    [Collection("DbBoundTest")]
    public class ApiHostsTests : IClassFixture<HttpServerFixture>
    {
        private readonly HttpServerFixture _httpServer;
        private readonly TestDbFixture _testDb;

        public ApiHostsTests(HttpServerFixture httpServer, TestDbFixture testDb)
        {
            _httpServer = httpServer;
            _testDb = testDb;
        }

        [Fact(DisplayName = "api/hosts GET")]
        public void Should_return_the_list_of_available_hosts()
        {
            using (var ctx = _testDb.CreateContext())
            {
                ctx.CrawlHostSettings.AddRange(new[]
                {
                    new CrawlHostSetting {CrawlDelay = 1, Disallow = "*", Host = "0"},
                    new CrawlHostSetting {CrawlDelay = 2, Disallow = "*", Host = "1"},
                    new CrawlHostSetting {CrawlDelay = 3, Disallow = "*", Host = "2"}
                });
                ctx.SaveChanges();

                using (var response = _httpServer.Get("api/hosts"))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    var content = response.Content as ObjectContent<IList<CrawlHostSetting>>;
                    Assert.NotNull(content);

                    var result = content.Value as IList<CrawlHostSetting>;
                    Assert.NotNull(result);

                    Assert.Equal(3, result.Count);
                }
            }
        }

        [Fact(DisplayName = "api/hosts/id GET")]
        public void Should_return_host_by_id()
        {
            using (var ctx = _testDb.CreateContext())
            {
                ctx.CrawlHostSettings.AddRange(new[]
                {
                    new CrawlHostSetting {CrawlDelay = 1, Disallow = "*", Host = "0"},
                    new CrawlHostSetting {CrawlDelay = 2, Disallow = "/", Host = "1"},
                    new CrawlHostSetting {CrawlDelay = 3, Disallow = "*", Host = "2"}
                });
                ctx.SaveChanges();
                var targetId = ctx.CrawlHostSettings.Single(s => s.Host == "1").Id;

                using (var response = _httpServer.Get("api/hosts/" + targetId))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    var content = response.Content as ObjectContent<CrawlHostSetting>;
                    Assert.NotNull(content);

                    var result = content.Value as CrawlHostSetting;
                    Assert.NotNull(result);

                    Assert.Equal(2, result.CrawlDelay);
                    Assert.Equal("/", result.Disallow);
                    Assert.Equal("1", result.Host);
                }
            }
        }

        [Fact(DisplayName = "api/hosts/default GET")]
        public void Should_return_default_settings()
        {
            using (var ctx = _testDb.CreateContext())
            {
                using (var response = _httpServer.Get("api/hosts/default"))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    var content = response.Content as ObjectContent<CrawlHostSetting>;
                    Assert.NotNull(content);

                    var result = content.Value as CrawlHostSetting;
                    Assert.NotNull(result);

                    Assert.Equal(60, result.CrawlDelay);
                    Assert.Equal("*", result.Disallow);
                    Assert.Equal("", result.Host);
                }
            }
        }

        [Fact(DisplayName = "api/hosts/default PUT")]
        public void Should_return_save_settings()
        {
            using (var ctx = _testDb.CreateContext())
            {
                var payload = JsonConvert.SerializeObject(new HostDefaultSettings
                {
                    Delay = 20,
                    Disallow = "/"
                });
                using (var response = _httpServer.PutJson("api/hosts/default", payload))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }

                var defaultSettings = ctx.CrawlHostSettings.Single(s => s.Host == string.Empty);
                Assert.Equal(20, defaultSettings.CrawlDelay);
                Assert.Equal("/", defaultSettings.Disallow);
            }
        }
    }
}