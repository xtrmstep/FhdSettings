using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using SettingsService.Api.Models;
using SettingsService.Api.Tests.Fixtures;
using SettingsService.Core.Data.Models;
using SettingsService.Impl;
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
            #region arrange

            using (var ctx = new SettingDbContext())
            {
                ctx.CrawlUrlSeeds.AddRange(new[]
                {
                    new CrawlUrlSeed {Url = "0"},
                    new CrawlUrlSeed {Url = "1"},
                    new CrawlUrlSeed {Url = "2"}
                });
                ctx.SaveChanges();
            }

            #endregion

            try
            {
                using (var response = _httpServer.Get("api/urls"))
                {
                    #region assert

                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    var content = response.Content as ObjectContent<IList<CrawlUrlSeed>>;
                    Assert.NotNull(content);

                    var result = content.Value as IList<CrawlUrlSeed>;
                    Assert.NotNull(result);

                    Assert.Equal(3, result.Count);

                    #endregion
                }
            }
            finally
            {
                #region remove data from db

                using (var ctx = new SettingDbContext())
                {
                    ctx.CrawlUrlSeeds.RemoveRange(ctx.CrawlUrlSeeds.AsQueryable());
                    ctx.SaveChanges();
                }

                #endregion
            }

        }

        [Fact(DisplayName = "api/urls POST")]
        public void Should_add_new_url()
        {
            // arrange
            var payload = JsonConvert.SerializeObject(new CrawlUrlSeed
            {
                Url = "0"
            });

            Guid result;
            using (var response = _httpServer.PostJson("api/urls", payload))
            {
                #region assert api

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                var content = response.Content as ObjectContent<Guid>;
                result = (Guid) content.Value;

                var expectedLocation = _httpServer.GetUrl("api/urls/" + result);
                Assert.Equal(expectedLocation, response.Headers.Location.ToString());

                #endregion
            }

            try
            {
                #region assert data

                using (var ctx = new SettingDbContext())
                {
                    var url = ctx.CrawlUrlSeeds.Single(s => s.Id == result);
                    Assert.Equal("0", url.Url);
                }

                #endregion
            }
            finally
            {
                #region remove data from db

                using (var ctx = new SettingDbContext())
                {
                    ctx.CrawlUrlSeeds.RemoveRange(ctx.CrawlUrlSeeds.AsQueryable());
                    ctx.SaveChanges();
                }

                #endregion
            }
        }

        [Fact(DisplayName = "api/urls/id GET")]
        public void Should_return_url_by_id()
        {
            #region arrange

            Guid targetId;
            using (var ctx = new SettingDbContext())
            {
                ctx.CrawlUrlSeeds.AddRange(new[]
                {
                    new CrawlUrlSeed {Url = "0"},
                    new CrawlUrlSeed {Url = "1"},
                    new CrawlUrlSeed {Url = "2"}
                });
                ctx.SaveChanges();
                targetId = ctx.CrawlUrlSeeds.Single(s => s.Url == "1").Id;
            }

            #endregion

            try
            {
                // act
                using (var response = _httpServer.Get("api/urls/" + targetId))
                {
                    #region assert

                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    var content = response.Content as ObjectContent<CrawlUrlSeed>;
                    Assert.NotNull(content);

                    var result = content.Value as CrawlUrlSeed;
                    Assert.NotNull(result);

                    Assert.Equal("1", result.Url);

                    #endregion
                }
            }
            finally
            {
                #region remove data from db

                using (var ctx = new SettingDbContext())
                {
                    ctx.CrawlUrlSeeds.RemoveRange(ctx.CrawlUrlSeeds.AsQueryable());
                    ctx.SaveChanges();
                }

                #endregion
            }
        }

        [Fact(DisplayName = "api/urls/id PUT")]
        public void Should_reject_update_request_to_url()
        {
            var anyGuid = Guid.NewGuid();
            var payload = JsonConvert.SerializeObject(new CrawlUrlSeed
            {
                Id = anyGuid,
                Url = "new"
            });
            using (var response = _httpServer.PutJson("api/urls/" + anyGuid, payload))
            {
                Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
            }
        }

        [Fact(DisplayName = "api/urls/id DELETE")]
        public void Should_delete_url_by_id()
        {
            Guid targetId;
            using (var ctx = new SettingDbContext())
            {
                ctx.CrawlUrlSeeds.AddRange(new[]
                {
                    new CrawlUrlSeed {Url = "0"},
                    new CrawlUrlSeed {Url = "1"},
                    new CrawlUrlSeed {Url = "2"}
                });
                ctx.SaveChanges();
                targetId = ctx.CrawlUrlSeeds.Single(s => s.Url == "1").Id;
            }
            try
            {
                using (var response = _httpServer.Delete("api/urls/" + targetId))
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
                using (var verifyCtx = new SettingDbContext())
                {
                    var url = verifyCtx.CrawlUrlSeeds.SingleOrDefault(s => s.Id == targetId);
                    Assert.Null(url);
                }
            }
            finally
            {
                #region remove data from db

                using (var ctx = new SettingDbContext())
                {
                    ctx.CrawlUrlSeeds.RemoveRange(ctx.CrawlUrlSeeds.AsQueryable());
                    ctx.SaveChanges();
                }

                #endregion
            }
        }
    }
}