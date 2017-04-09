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

namespace SettingsService.Api.Tests.Endpoints
{
    [Collection("DbBoundTest")]
    public class ApiRulesTests : IClassFixture<HttpServerFixture>
    {
        private readonly HttpServerFixture _httpServer;
        private readonly TestDbFixture _testDb;

        public ApiRulesTests(HttpServerFixture httpServer, TestDbFixture testDb)
        {
            _httpServer = httpServer;
            _testDb = testDb;
        }

        [Fact(DisplayName = "api/rules GET")]
        public void Should_return_list_of_rules()
        {
            using (var ctx = _testDb.CreateContext())
            {
                ctx.ExtractRules.AddRange(new[]
                {
                    new ExtractRule {Name = "1", DataType = ExtratorDataType.Link, RegExpression = "expr1"},
                    new ExtractRule {Name = "2", DataType = ExtratorDataType.Picture, RegExpression = "expr2"},
                    new ExtractRule {Name = "3", DataType = ExtratorDataType.Video, RegExpression = "expr3"}
                });
                ctx.SaveChanges();

                using (var response = _httpServer.Get("api/rules"))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    var content = response.Content as ObjectContent<IList<ExtractRuleReadModel>>;
                    Assert.NotNull(content);

                    var result = content.Value as IList<ExtractRuleReadModel>;
                    Assert.NotNull(result);

                    Assert.Equal(3, result.Count);
                }
            }
        }

        [Fact(DisplayName = "api/rules POST")]
        public void Should_add_new_rule()
        {
            using (var ctx = _testDb.CreateContext())
            {
                var payload = JsonConvert.SerializeObject(new ExtractRuleModel
                {
                    Name = "Name1",
                    DataType = ExtratorDataType.Link.ToString(),
                    RegExpression = "expr1"
                });

                Guid result;
                using (var response = _httpServer.PostJson("api/rules", payload))
                {
                    Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                    var content = response.Content as ObjectContent<Guid>;
                    result = (Guid) content.Value;

                    var expectedLocation = _httpServer.GetUrl("api/rules/" + result);
                    Assert.Equal(expectedLocation, response.Headers.Location.ToString());
                }

                var rule = ctx.ExtractRules.Single(s => s.Id == result);
                Assert.Equal("Name1", rule.Name);
            }
        }

        [Fact(DisplayName = "api/rules/id GET")]
        public void Should_return_single_rule_by_id()
        {
            using (var ctx = _testDb.CreateContext())
            {
                ctx.ExtractRules.AddRange(new[]
                {
                    new ExtractRule {Name = "1", DataType = ExtratorDataType.Link, RegExpression = "expr1"},
                    new ExtractRule {Name = "2", DataType = ExtratorDataType.Picture, RegExpression = "expr2"},
                    new ExtractRule {Name = "3", DataType = ExtratorDataType.Video, RegExpression = "expr3"}
                });
                ctx.SaveChanges();
                var targetId = ctx.ExtractRules.Single(s => s.Name == "1").Id;

                using (var response = _httpServer.Get("api/rules/" + targetId))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    var content = response.Content as ObjectContent<ExtractRuleReadModel>;
                    Assert.NotNull(content);

                    var result = content.Value as ExtractRuleModel;
                    Assert.NotNull(result);

                    Assert.Equal("1", result.Name);
                    Assert.Equal("Link", result.DataType);
                }
            }
        }

        [Fact(DisplayName = "api/rules/id PUT")]
        public void Should_update_rule_with_id()
        {
            using (var ctx = _testDb.CreateContext())
            {
                ctx.ExtractRules.AddRange(new[]
                {
                    new ExtractRule {Name = "1", DataType = ExtratorDataType.Link, RegExpression = "expr1"},
                    new ExtractRule {Name = "2", DataType = ExtratorDataType.Picture, RegExpression = "expr2"},
                    new ExtractRule {Name = "3", DataType = ExtratorDataType.Video, RegExpression = "expr3"}
                });
                ctx.SaveChanges();
                var targetId = ctx.ExtractRules.Single(s => s.Name == "2").Id;

                var payload = JsonConvert.SerializeObject(new ExtractRule
                {
                    Id = targetId,
                    RegExpression = "new_expr2",
                    Name = "2",
                    DataType = ExtratorDataType.Picture
                });
                using (var response = _httpServer.PutJson("api/rules/" + targetId, payload))
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

        [Fact(DisplayName = "api/rules/id DELETE")]
        public void Should_delete_rule_with_id()
        {
            using (var ctx = _testDb.CreateContext())
            {
                ctx.ExtractRules.AddRange(new[]
                {
                    new ExtractRule {Name = "1", DataType = ExtratorDataType.Link, RegExpression = "expr1"},
                    new ExtractRule {Name = "2", DataType = ExtratorDataType.Picture, RegExpression = "expr2"},
                    new ExtractRule {Name = "3", DataType = ExtratorDataType.Video, RegExpression = "expr3"}
                });
                ctx.SaveChanges();
                var targetId = ctx.ExtractRules.Single(s => s.Name == "2").Id;

                using (var response = _httpServer.Delete("api/rules/" + targetId))
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