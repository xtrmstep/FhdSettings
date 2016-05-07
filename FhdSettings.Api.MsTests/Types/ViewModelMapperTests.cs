using System;
using FhdSettings.Api.Models.Auth;
using FhdSettings.Api.Types;
using FhdSettings.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FhdSettings.Api.MsTests.Types
{
    [TestClass]
    public class ViewModelMapperTests
    {
        [TestMethod]
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

            Assert.IsNotNull(model);
            Assert.AreEqual(model.Token, tokenString);
            Assert.AreEqual(model.Expires, expiresDate);
        }
    }
}
