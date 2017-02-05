using System;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using SettingsService.Impl;
using SettingsService.Impl.Migrations;

namespace SettingsService.Api.Tests.Fixtures
{
    public class TestDbFixture
    {
        public TestDbFixture()
        {
            // make sure DB is created and up-to-date
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SettingDbContext, Configuration>());
            using (var ctx = new SettingDbContext())
            {
                // make a call to DB in order to migrate it to the latest version
                var r = ctx.CrawlRules.Take(1).ToList();
            }
        }

        // note: it's not used because of the AppVeyor issue
        // issue description https://stackoverflow.com/questions/42057471/transactionscope-in-xunit-tests-does-not-work-on-appveyor
        public SettingDbContext CreateContext()
        {
            return new TestDbContext().Context;
        }

        private class TestDbContext : IDisposable
        {
            private readonly TransactionScope _transaction;

            internal TestDbContext()
            {
                // create a transaction scope
                _transaction = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions {IsolationLevel = IsolationLevel.ReadUncommitted});
                Context = new SettingDbContext();
            }

            public SettingDbContext Context { get; }

            public void Dispose()
            {
                Context.Dispose();
                _transaction.Dispose();
            }
        }
    }
}