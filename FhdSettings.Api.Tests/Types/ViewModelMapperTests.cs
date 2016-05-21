using System;
using FhdSettings.Api.Models.Auth;
using FhdSettings.Api.Types;
using FhdSettings.Data.Models;
using Xunit;

namespace FhdSettings.Api.MsTests.Types
{
    public class ViewModelMapperTests
    {
        [Fact]
        public void Should_map_from_AuthToken_to_ViewModel()
        {
            string tokenString = "test";
            DateTimeOffset expiresDate = new DateTimeOffset(2016, 01, 01, 0, 0, 1, TimeSpan.Zero);

            var entity = new AuthToken
            {
                Token = tokenString,
                Expires = expiresDate
            };
            var model = ViewModelMapper.Map<AuthToken, AuthTokenViewModel>(entity);

            Assert.NotNull(model);
            Assert.Equal(tokenString, model.Token);
            Assert.Equal(expiresDate, model.Expires);
        }
    }
}
