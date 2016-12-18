using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
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

                using (var response = _httpServer.GetJson("api/hosts/" + id))
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
                    new CrawlHostSetting {CrawlDelay = 3, Disallow = "*", Host = "2", Id = Guid.NewGuid()}
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

        [Fact(DisplayName = "api/hosts/id: should return host by id")]
        public void Should_return_host_by_id()
        {
            using (var ctx = _testDb.CreateContext())
            {
                var targetId = Guid.NewGuid();
                ctx.CrawlHostSettings.AddRange(new[]
                {
                    new CrawlHostSetting {CrawlDelay = 1, Disallow = "*", Host = "", Id = Guid.Empty},
                    new CrawlHostSetting {CrawlDelay = 2, Disallow = "/", Host = "1", Id = targetId},
                    new CrawlHostSetting {CrawlDelay = 3, Disallow = "*", Host = "2", Id = Guid.NewGuid()}
                });
                ctx.SaveChanges();

                using (var response = _httpServer.GetJson("api/hosts/" + targetId))
                {
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

        [Fact(DisplayName = "api/hosts/default: should return defaults")]
        public void Should_return_default_settings()
        {
            using (var ctx = _testDb.CreateContext())
            {
                using (var response = _httpServer.GetJson("api/hosts/default"))
                {
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

        [Fact(DisplayName = "api/hosts/default: should save defaults")]
        public void Should_return_save_settings()
        {
            using (var ctx = _testDb.CreateContext())
            {
                var payload = JsonConvert.SerializeObject(new CrawlHostSetting
                {
                    CrawlDelay = 20, // new value
                    Disallow = "/", // new value
                    Host = "",
                    Id = Guid.NewGuid()
                });
                using (var response = _httpServer.PutJson("api/hosts/default", payload))
                {
                    var content = response.Content as ObjectContent<CrawlHostSetting>;
                    Assert.NotNull(content);

                    var result = content.Value as CrawlHostSetting;
                    Assert.NotNull(result);

                    Assert.Equal(20, result.CrawlDelay);
                    Assert.Equal("/", result.Disallow);
                    Assert.Equal("", result.Host);
                }
            }
        }
    }
}