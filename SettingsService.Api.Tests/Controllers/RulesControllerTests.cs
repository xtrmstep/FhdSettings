using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using SettingsService.Api.Tests.Fixtures;
using SettingsService.Core.Data.Models;
using SettingsService.Impl;
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
            using (var ctx = new SettingDbContext())
            {
                ctx.CrawlRules.AddRange(new[]
                {
                    new CrawlRule {DataType = CrawlDataBlockType.Link, Host = "test", Name = "Link"},
                    new CrawlRule {DataType = CrawlDataBlockType.Picture, Host = "test", Name = "Picture"},
                    new CrawlRule {DataType = CrawlDataBlockType.Video, Host = "test", Name = "Video"}
                });
                ctx.SaveChanges();
            }
            try
            {
                using (var response = _httpServer.Get("api/crawler/rules?host=test"))
                {
                    var content = response.Content as ObjectContent<IList<CrawlRule>>;
                    Assert.NotNull(content);

                    var result = content.Value as IList<CrawlRule>;
                    Assert.NotNull(result);

                    Assert.True(result.Any());
                }
            }
            finally
            {
                #region remove data from db

                using (var ctx = new SettingDbContext())
                {
                    ctx.CrawlRules.RemoveRange(ctx.CrawlRules.AsQueryable());
                    ctx.SaveChanges();
                }

                #endregion
            }
        }
    }
}