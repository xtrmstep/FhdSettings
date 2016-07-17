using SettingsService.Core.Utilities;
using Xunit;

namespace FhdSettings.Tests.Utilities
{
    public class StringExtensionsTests
    {
        [Theory(DisplayName = "Should return Host from URL")]
        [InlineData("sub.domain.com/folder?p1=v1", "sub.domain.com")]
        [InlineData("www.sub.domain.com/folder?p1=v1", "sub.domain.com")]
        [InlineData("http://sub.domain.com/folder?p1=v1", "sub.domain.com")]
        [InlineData("https://sub.domain.com/folder?p1=v1", "sub.domain.com")]
        [InlineData("https://www.sub.domain.com/folder?p1=v1", "sub.domain.com")]
        [InlineData("domain.com/folder?p1=v1", "domain.com")]
        [InlineData("www.domain.com/folder?p1=v1", "domain.com")]
        [InlineData("http://domain.com/folder?p1=v1", "domain.com")]
        [InlineData("https://domain.com/folder?p1=v1", "domain.com")]
        [InlineData("https://www.domain.com/folder?p1=v1", "domain.com")]
        [InlineData("sub.sub.domain.com/folder?p1=v1", "sub.sub.domain.com")]
        [InlineData("www.sub.sub.domain.com/folder?p1=v1", "sub.sub.domain.com")]
        [InlineData("http://sub.sub.domain.com/folder?p1=v1", "sub.sub.domain.com")]
        [InlineData("https://sub.sub.domain.com/folder?p1=v1", "sub.sub.domain.com")]
        [InlineData("https://www.sub.sub.domain.com/folder?p1=v1", "sub.sub.domain.com")]
        public void Should_return_host_from_url(string input, string expected)
        {
            var actual = input.GetHost();
            Assert.Equal(expected, actual);
        }
    }
}