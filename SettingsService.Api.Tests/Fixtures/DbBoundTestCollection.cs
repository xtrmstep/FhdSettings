using Xunit;

namespace SettingsService.Api.Tests.Fixtures
{
    [CollectionDefinition("DbBoundTest")]
    public class DbBoundTestCollection : ICollectionFixture<TestDbFixture>
    {
    }
}