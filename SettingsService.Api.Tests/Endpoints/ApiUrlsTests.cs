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
    public class ApiUrlsTests : IClassFixture<HttpServerFixture>
    {
        private readonly HttpServerFixture _httpServer;
        private readonly TestDbFixture _testDb;

        public ApiUrlsTests(HttpServerFixture httpServer, TestDbFixture testDb)
        {
            _httpServer = httpServer;
            _testDb = testDb;
        }

        [Fact(DisplayName = "api/urls GET")]
        public void Should_return_the_list_of_urls()
        {
            using (var ctx = _testDb.CreateContext())
            {
                ctx.Hosts.AddRange(new[]
                {
                    new Host{SeedUrl = "0"},
                    new Host{SeedUrl = "1"},
                    new Host{SeedUrl = "2"}
                });
                ctx.SaveChanges();

                using (var response = _httpServer.Get("api/urls"))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    var content = response.Content as ObjectContent<IList<Host>>;
                    Assert.NotNull(content);

                    var result = content.Value as IList<Host>;
                    Assert.NotNull(result);

                    Assert.Equal(3, result.Count);
                }
            }
        }

        [Fact(DisplayName = "api/urls POST")]
        public void Should_add_new_url()
        {
            using (var ctx = _testDb.CreateContext())
            {
                var payload = JsonConvert.SerializeObject(new Host
                {
                    SeedUrl = "0"
                });

                Guid result;
                using (var response = _httpServer.PostJson("api/urls", payload))
                {
                    Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                    var content = response.Content as ObjectContent<Guid>;
                    result = (Guid)content.Value;

                    var expectedLocation = _httpServer.GetUrl("api/urls/" + result);
                    Assert.Equal(expectedLocation, response.Headers.Location.ToString());
                }

                var url = ctx.Hosts.Single(s => s.Id == result);
                Assert.Equal("0", url.SeedUrl);
            }
        }

        [Fact(DisplayName = "api/urls/id GET")]
        public void Should_return_url_by_id()
        {
            using (var ctx = _testDb.CreateContext())
            {
                ctx.Hosts.AddRange(new[]
                {
                    new Host {SeedUrl = "0"},
                    new Host {SeedUrl = "1"},
                    new Host {SeedUrl = "2"}
                });
                ctx.SaveChanges();
                var targetId = ctx.Hosts.Single(s => s.SeedUrl == "1").Id;

                using (var response = _httpServer.Get("api/urls/" + targetId))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    var content = response.Content as ObjectContent<Host>;
                    Assert.NotNull(content);

                    var result = content.Value as Host;
                    Assert.NotNull(result);

                    Assert.Equal("1", result.SeedUrl);
                }
            }
        }

        [Fact(DisplayName = "api/urls/id PUT")]
        public void Should_reject_update_request_to_url()
        {
            var anyGuid = Guid.NewGuid();
            var payload = JsonConvert.SerializeObject(new Host
            {
                Id = anyGuid,
                SeedUrl = "new"
            });
            using (var response = _httpServer.PutJson("api/urls/" + anyGuid, payload))
            {
                Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
            }
        }

        [Fact(DisplayName = "api/urls/id DELETE")]
        public void Should_delete_url_by_id()
        {
            using (var ctx = _testDb.CreateContext())
            {
                ctx.Hosts.AddRange(new[]
                {
                    new Host {SeedUrl = "0"},
                    new Host {SeedUrl = "1"},
                    new Host {SeedUrl = "2"}
                });
                ctx.SaveChanges();
                var targetId = ctx.Hosts.Single(s => s.SeedUrl == "1").Id;
                using (var response = _httpServer.Delete("api/urls/" + targetId))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
                using (var verifyCtx = _testDb.CreateContext())
                {
                    var url = verifyCtx.Hosts.SingleOrDefault(s => s.Id == targetId);
                    Assert.Null(url);
                }
            }
        }
    }
}