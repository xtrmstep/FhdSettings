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

        public ApiCrawlerRulesTests(HttpServerFixture httpServer, TestDbFixture testDb)
        {
            _httpServer = httpServer;
            _testDb = testDb;
        }

        [Fact(DisplayName = "api/crawler/rules GET")]
        public void Should_return_list_of_rules()
        {
            using (var ctx = _testDb.CreateContext())
            {
                var host = new Host {SeedUrl = "1"};
                ctx.Hosts.Add(host);
                ctx.ExtractRules.AddRange(new[]
                {
                    new ExtractRule{Name = "1",DataType = ExtratorDataType.Link, Host = host, RegExpression = "expr1"},
                    new ExtractRule{Name = "2",DataType = ExtratorDataType.Picture, Host = host, RegExpression = "expr2"},
                    new ExtractRule{Name = "3",DataType = ExtratorDataType.Video, Host = host, RegExpression = "expr3"},
                });
                ctx.SaveChanges();

                using (var response = _httpServer.Get("api/crawler/rules?host=1"))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    var content = response.Content as ObjectContent<IList<ExtractRule>>;
                    Assert.NotNull(content);

                    var result = content.Value as IList<ExtractRule>;
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
                var hostEmpty = new Host {SeedUrl = string.Empty};
                var hostDef = new Host {SeedUrl = "def"};
                ctx.Hosts.AddRange(new[]
                {
                    hostEmpty,
                    hostDef
                });
                ctx.ExtractRules.AddRange(new[]
                {
                    new ExtractRule{Name = "1",DataType = ExtratorDataType.Link, Host = hostEmpty, RegExpression = "expr1"},
                    new ExtractRule{Name = "2",DataType = ExtratorDataType.Picture, Host = hostDef, RegExpression = "expr2"},
                    new ExtractRule{Name = "3",DataType = ExtratorDataType.Video, Host = hostEmpty, RegExpression = "expr3"},
                });
                ctx.SaveChanges();

                using (var response = _httpServer.Get("api/crawler/rules/default"))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    var content = response.Content as ObjectContent<IList<ExtractRule>>;
                    Assert.NotNull(content);

                    var result = content.Value as IList<ExtractRule>;
                    Assert.NotNull(result);

                    Assert.Equal(2, result.Count);
                }
            }
        }

        [Fact(DisplayName = "api/crawler/rules POST")]
        public void Should_add_new_rule()
        {
            using (var ctx = _testDb.CreateContext())
            {
                var payload = JsonConvert.SerializeObject(new ExtractRule
                {
                    Name = "Name1",
                    DataType = ExtratorDataType.Link,
                    Host = new Host { SeedUrl = "Host1" },
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

                var rule = ctx.ExtractRules.Single(s => s.Id == result);
                Assert.Equal("Name1", rule.Name);
            }
        }

        [Fact(DisplayName = "api/crawler/rules/id GET")]
        public void Should_return_single_rule_by_id()
        {
            using (var ctx = _testDb.CreateContext())
            {
                var host1 = new Host {SeedUrl = "1"};
                var host2 = new Host {SeedUrl = "2"};
                var host3 = new Host {SeedUrl = "3"};
                ctx.Hosts.AddRange(new[] { host1, host2, host3 });
                ctx.ExtractRules.AddRange(new[]
                {
                    new ExtractRule{Name = "1",DataType = ExtratorDataType.Link, Host = host1, RegExpression = "expr1"},
                    new ExtractRule{Name = "2",DataType = ExtratorDataType.Picture, Host = host2, RegExpression = "expr2"},
                    new ExtractRule{Name = "3",DataType = ExtratorDataType.Video, Host = host3, RegExpression = "expr3"},
                });
                ctx.SaveChanges();
                var targetId = ctx.ExtractRules.Single(s => s.Name == "2").Id;

                using (var response = _httpServer.Get("api/crawler/rules/"+ targetId))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    var content = response.Content as ObjectContent<ExtractRule>;
                    Assert.NotNull(content);

                    var result = content.Value as ExtractRule;
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
                var host1 = new Host { SeedUrl = "1" };
                var host2 = new Host { SeedUrl = "2" };
                var host3 = new Host { SeedUrl = "3" };
                ctx.Hosts.AddRange(new[] { host1, host2, host3 });
                ctx.ExtractRules.AddRange(new[]
                {
                    new ExtractRule{Name = "1",DataType = ExtratorDataType.Link, Host = host1, RegExpression = "expr1"},
                    new ExtractRule{Name = "2",DataType = ExtratorDataType.Picture, Host = host2, RegExpression = "expr2"},
                    new ExtractRule{Name = "3",DataType = ExtratorDataType.Video, Host = host3, RegExpression = "expr3"},
                });
                ctx.SaveChanges();
                var targetId = ctx.ExtractRules.Single(s => s.Name == "2").Id;

                var payload = JsonConvert.SerializeObject(new ExtractRule
                {
                    Id = targetId,
                    RegExpression = "new_expr2",
                    Name = "2",
                    DataType = ExtratorDataType.Picture,
                    Host = host2
                });
                using (var response = _httpServer.PutJson("api/crawler/rules/" + targetId, payload))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
                using (var verifyCtx = _testDb.CreateContext())
                {
                    var rule = verifyCtx.ExtractRules.Single(s => s.RegExpression == "new_expr2");
                    Assert.Equal("2", rule.Name);
                }
            }
        }

        [Fact(DisplayName = "api/crawler/rules/id DELETE")]
        public void Should_delete_rule_with_id()
        {
            using (var ctx = _testDb.CreateContext())
            {
                var host1 = new Host { SeedUrl = "1" };
                var host2 = new Host { SeedUrl = "2" };
                var host3 = new Host { SeedUrl = "3" };
                ctx.Hosts.AddRange(new[] { host1, host2, host3 });
                ctx.ExtractRules.AddRange(new[]
                {
                    new ExtractRule{Name = "1",DataType = ExtratorDataType.Link, Host = host1, RegExpression = "expr1"},
                    new ExtractRule{Name = "2",DataType = ExtratorDataType.Picture, Host = host2, RegExpression = "expr2"},
                    new ExtractRule{Name = "3",DataType = ExtratorDataType.Video, Host = host3, RegExpression = "expr3"},
                });
                ctx.SaveChanges();
                var targetId = ctx.ExtractRules.Single(s => s.Name == "2").Id;

                using (var response = _httpServer.Delete("api/crawler/rules/" + targetId))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
                using (var verifyCtx = _testDb.CreateContext())
                {
                    var rule = verifyCtx.ExtractRules.SingleOrDefault(s => s.Name == "2");
                    Assert.Null(rule);
                }
            }
        }
    }
}