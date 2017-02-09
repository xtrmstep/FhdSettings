using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using SettingsService.Api.Models;
using SettingsService.Api.Tests.Fixtures;
using SettingsService.Core.Data.Models;
using Xunit;
using Xunit.Abstractions;

namespace SettingsService.Api.Tests.Controllers
{
    [Collection("DbBoundTest")]
    public class ApiCrawlerRulesTests : IClassFixture<HttpServerFixture>
    {
        private readonly HttpServerFixture _httpServer;
        private readonly TestDbFixture _testDb;
        private ITestOutputHelper _output;

        public ApiCrawlerRulesTests(HttpServerFixture httpServer, TestDbFixture testDb, ITestOutputHelper output)
        {
            _httpServer = httpServer;
            _testDb = testDb;
            _output = output;
        }

        [Fact(DisplayName = "api/crawler/rules GET")]
        public void Should_return_list_of_rules()
        {
            using (var ctx = _testDb.CreateContext())
            {
                ctx.CrawlRules.AddRange(new[]
                {
                    new CrawlRule{Name = "1",DataType = CrawlDataBlockType.Link, Host = "1", RegExpression = "expr1"},
                    new CrawlRule{Name = "2",DataType = CrawlDataBlockType.Picture, Host = "2", RegExpression = "expr2"},
                    new CrawlRule{Name = "3",DataType = CrawlDataBlockType.Video, Host = "3", RegExpression = "expr3"},
                });
                ctx.SaveChanges();

                using (var response = _httpServer.Get("api/crawler/rules"))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    var content = response.Content as ObjectContent<IList<CrawlRule>>;
                    Assert.NotNull(content);

                    var result = content.Value as IList<CrawlRule>;
                    Assert.NotNull(result);

                    Assert.Equal(3, result.Count);
                }
            }
        }

        [Fact(DisplayName = "api/crawler/rules/default GET")]
        public void Should_return_list_of_default_rules()
        {
            using (var ctx = _testDb.CreateContext())
            {
                // host should be empty for default rules
                ctx.CrawlRules.AddRange(new[]
                {
                    new CrawlRule{Name = "1",DataType = CrawlDataBlockType.Link, Host = string.Empty, RegExpression = "expr1"},
                    new CrawlRule{Name = "2",DataType = CrawlDataBlockType.Picture, Host = "def", RegExpression = "expr2"},
                    new CrawlRule{Name = "3",DataType = CrawlDataBlockType.Video, Host = string.Empty, RegExpression = "expr3"},
                });
                ctx.SaveChanges();

                using (var response = _httpServer.Get("api/crawler/rules/default"))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    var content = response.Content as ObjectContent<IList<CrawlRule>>;
                    Assert.NotNull(content);

                    var result = content.Value as IList<CrawlRule>;
                    Assert.NotNull(result);

                    Assert.Equal(3, result.Count);
                }
            }
        }

        [Fact(DisplayName = "api/crawler/rules POST")]
        public void Should_add_new_rule()
        {
            using (var ctx = _testDb.CreateContext())
            {
                var payload = JsonConvert.SerializeObject(new CrawlRule
                {
                    Name = "Name1",
                    DataType = CrawlDataBlockType.Link,
                    Host = "Host1",
                    RegExpression = "expr1"
                });

                Guid result;
                using (var response = _httpServer.PostJson("api/crawler/rules", payload))
                {
                    Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                    var content = response.Content as ObjectContent<Guid>;
                    result = (Guid)content.Value;

                    var expectedLocation = _httpServer.GetUrl("api/crawler/rules/" + result);
                    Assert.Equal(expectedLocation, response.Headers.Location.ToString());
                }

                var rule = ctx.CrawlRules.Single(s => s.Id == result);
                Assert.Equal("Name1", rule.Name);
            }
        }

        [Fact(DisplayName = "api/crawler/rules/id GET")]
        public void Should_return_single_rule_by_id()
        {
            using (var ctx = _testDb.CreateContext())
            {
                ctx.CrawlRules.AddRange(new[]
                {
                    new CrawlRule{Name = "1",DataType = CrawlDataBlockType.Link, Host = "1", RegExpression = "expr1"},
                    new CrawlRule{Name = "2",DataType = CrawlDataBlockType.Picture, Host = "2", RegExpression = "expr2"},
                    new CrawlRule{Name = "3",DataType = CrawlDataBlockType.Video, Host = "3", RegExpression = "expr3"},
                });
                ctx.SaveChanges();
                var targetId = ctx.CrawlRules.Single(s => s.Name == "2").Id;

                using (var response = _httpServer.Get("api/crawler/rules/"+ targetId))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    var content = response.Content as ObjectContent<CrawlRule>;
                    Assert.NotNull(content);

                    var result = content.Value as CrawlRule;
                    Assert.NotNull(result);

                    Assert.Equal("2", result.Name);
                }
            }
        }

        [Fact(DisplayName = "api/crawler/rules/id PUT")]
        public void Should_update_rule_with_id()
        {
            using (var ctx = _testDb.CreateContext())
            {
                ctx.CrawlRules.AddRange(new[]
                {
                    new CrawlRule{Name = "1",DataType = CrawlDataBlockType.Link, Host = "1", RegExpression = "expr1"},
                    new CrawlRule{Name = "2",DataType = CrawlDataBlockType.Picture, Host = "2", RegExpression = "expr2"},
                    new CrawlRule{Name = "3",DataType = CrawlDataBlockType.Video, Host = "3", RegExpression = "expr3"},
                });
                ctx.SaveChanges();
                var targetId = ctx.CrawlRules.Single(s => s.Name == "2").Id;

                var payload = JsonConvert.SerializeObject(new CrawlRule
                {
                    Id = targetId,
                    RegExpression = "new_expr2",
                    Name = "2",
                    DataType = CrawlDataBlockType.Picture,
                    Host = "2"
                });
                using (var response = _httpServer.PutJson("api/crawler/rules/" + targetId, payload))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
                using (var verifyCtx = _testDb.CreateContext())
                {
                    var rule = verifyCtx.CrawlRules.Single(s => s.RegExpression == "new_expr2");
                    Assert.Equal("2", rule.Name);
                }
            }
        }

        [Fact(DisplayName = "api/crawler/rules/id DELETE")]
        public void Should_delete_rule_with_id()
        {
            // todo affected by another tests for default rules (parallelism??)
            // todo output is not captured
            _output.WriteLine("sfdgsdg");
            using (var ctx = _testDb.CreateContext())
            {
                ctx.CrawlRules.AddRange(new[]
                {
                    new CrawlRule{Name = "1",DataType = CrawlDataBlockType.Link, Host = "1", RegExpression = "expr1"},
                    new CrawlRule{Name = "2",DataType = CrawlDataBlockType.Picture, Host = "2", RegExpression = "expr2"},
                    new CrawlRule{Name = "3",DataType = CrawlDataBlockType.Video, Host = "3", RegExpression = "expr3"},
                });
                ctx.SaveChanges();
                var targetId = ctx.CrawlRules.Single(s => s.Name == "2").Id;

                using (var response = _httpServer.Delete("api/crawler/rules/" + targetId))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
                using (var verifyCtx = _testDb.CreateContext())
                {
                    var rule = verifyCtx.CrawlRules.SingleOrDefault(s => s.Name == "2");
                    Assert.Null(rule);
                }
            }
        }
    }
}