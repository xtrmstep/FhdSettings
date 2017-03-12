using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using SettingsService.Api.Tests.Fixtures;
using SettingsService.Core.Data.Models;
using Xunit;

namespace SettingsService.Api.Tests.Controllers
{
    [Collection("DbBoundTest")]
    public class RulesControllerTests : IClassFixture<HttpServerFixture>
    {
        private readonly HttpServerFixture _httpServer;
        private readonly TestDbFixture _testDb;

        public RulesControllerTests(HttpServerFixture httpServer, TestDbFixture testDb)
        {
            _httpServer = httpServer;
            _testDb = testDb;
        }

        [Fact(DisplayName = "GET: api/crawler/rules?host=test")]
        public void Should_return_list_of_rules()
        {
            using (var ctx = _testDb.CreateContext())
            {
                var host = new Host {SeedUrl = "test"};
                ctx.Hosts.Add(host);
                ctx.ExtractRules.AddRange(new[]
                {
                    new ExtractRule {DataType = ExtratorDataType.Link, Host = host, Name = "Link"},
                    new ExtractRule {DataType = ExtratorDataType.Picture, Host = host, Name = "Picture"},
                    new ExtractRule {DataType = ExtratorDataType.Video, Host = host, Name = "Video"}
                });
                ctx.SaveChanges();

                using (var response = _httpServer.Get("api/crawler/rules?host=test"))
                {
                    var content = response.Content as ObjectContent<IList<ExtractRule>>;
                    Assert.NotNull(content);

                    var result = content.Value as IList<ExtractRule>;
                    Assert.NotNull(result);

                    Assert.True(result.Any());
                }
            }
        }
    }
}