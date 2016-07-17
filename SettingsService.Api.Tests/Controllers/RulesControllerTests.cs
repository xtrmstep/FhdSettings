﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using SettingsService.Api.Tests.Fixtures;
using SettingsService.Core.Data.Models;
using Xunit;

namespace SettingsService.Api.Tests.Controllers
{
    public class RulesControllerTests : IClassFixture<HttpServerFixture>, IClassFixture<TestDbFixture>
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
                ctx.CrawlRules.AddRange(new[]
                {
                    new CrawlRule {DataType = CrawlDataBlockType.Link, Host = "test", Name = "Link"},
                    new CrawlRule {DataType = CrawlDataBlockType.Picture, Host = "test", Name = "Picture"},
                    new CrawlRule {DataType = CrawlDataBlockType.Video, Host = "test", Name = "Video"}
                });
                ctx.SaveChanges();

                using (var response = _httpServer.GetJson("api/crawler/rules?host=test"))
                {
                    var content = response.Content as ObjectContent<IList<CrawlRule>>;
                    Assert.NotNull(content);

                    var result = content.Value as IList<CrawlRule>;
                    Assert.NotNull(result);

                    Assert.True(result.Any());
                }
            }
        }
    }
}