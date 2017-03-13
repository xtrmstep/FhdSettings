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
    public class ApiSettingsTests : IClassFixture<HttpServerFixture>
    {
        private readonly HttpServerFixture _httpServer;
        private readonly TestDbFixture _testDb;

        public ApiSettingsTests(HttpServerFixture httpServer, TestDbFixture testDb)
        {
            _httpServer = httpServer;
            _testDb = testDb;
        }

        [Fact(DisplayName = "api/settings GET")]
        public void Should_return_all_settings()
        {
            using (var ctx = _testDb.CreateContext())
            {
                ctx.Settings.AddRange(new[]
                {
                    new Setting {Code = "code1", Name = "name1", Value = "value1"}, 
                    new Setting {Code = "code2", Name = "name2", Value = "value2"}, 
                });
                ctx.SaveChanges();

                using (var response = _httpServer.Get("api/settings"))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    var content = response.Content as ObjectContent<IList<Setting>>;
                    Assert.NotNull(content);

                    var result = content.Value as IList<Setting>;
                    Assert.NotNull(result);

                    // 2 default settings + 2 just added
                    Assert.Equal(4, result.Count);
                }
            }
        }

        [Fact(DisplayName = "api/settings POST")]
        public void Should_add_new_setting()
        {
            using (var ctx = _testDb.CreateContext())
            {
                var payload = JsonConvert.SerializeObject(new SettingModel { Code = "0" });

                Guid result;
                using (var response = _httpServer.PostJson("api/settings", payload))
                {
                    Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                    var content = response.Content as ObjectContent<Guid>;
                    result = (Guid)content.Value;

                    var expectedLocation = _httpServer.GetUrl("api/settings/" + result);
                    Assert.Equal(expectedLocation, response.Headers.Location.ToString());
                }

                var setting = ctx.Settings.Single(s => s.Id == result);
                Assert.Equal("0", setting.Code);
            }
        }

        [Fact(DisplayName = "api/settings/id PUT")]
        public void Should_update_setting_by_id()
        {
            using (var ctx = _testDb.CreateContext())
            {
                ctx.Settings.AddRange(new[]
                {
                    new Setting {Code = "code1", Name = "name1", Value = "value1"},
                    new Setting {Code = "code2", Name = "name2", Value = "value2"},
                });
                ctx.SaveChanges();
                var targetId = ctx.Settings.Single(s => s.Code == "code1").Id;

                var payload = JsonConvert.SerializeObject(new SettingModel
                {
                    Code = "11"
                });
                using (var response = _httpServer.PutJson("api/settings/" + targetId, payload))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
                using (var verifyCtx = _testDb.CreateContext())
                {
                    var setting = verifyCtx.Settings.Single(s => s.Id == targetId);
                    Assert.Equal("11", setting.Code);
                }
            }
        }

        [Fact(DisplayName = "api/settings/id DELETE")]
        public void Should_delete_setting_by_id()
        {
            using (var ctx = _testDb.CreateContext())
            {
                ctx.Settings.AddRange(new[]
                {
                    new Setting {Code = "code1", Name = "name1", Value = "value1"},
                    new Setting {Code = "code2", Name = "name2", Value = "value2"},
                });
                ctx.SaveChanges();
                var targetId = ctx.Settings.Single(s => s.Code == "code2").Id;
                using (var response = _httpServer.Delete("api/settings/" + targetId))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
                using (var verifyCtx = _testDb.CreateContext())
                {
                    var setting = verifyCtx.Settings.SingleOrDefault(s => s.Id == targetId);
                    Assert.Null(setting);
                }
            }
        }
    }
}