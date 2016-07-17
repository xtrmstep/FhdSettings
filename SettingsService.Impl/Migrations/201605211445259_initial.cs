using System.Data.Entity.Migrations;

namespace SettingsService.Impl.Migrations
{
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthTokens",
                c => new
                    {
                        ServiceCode = c.String(nullable: false, maxLength: 128),
                        Token = c.String(),
                        Expires = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.ServiceCode)
                .ForeignKey("dbo.ClinetServices", t => t.ServiceCode)
                .Index(t => t.ServiceCode);
            
            CreateTable(
                "dbo.ClinetServices",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Password = c.String(),
                        PasswordExpires = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.CrawlHostSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Host = c.String(),
                        Disallow = c.String(),
                        CrawlDelay = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CrawlRules",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        DataType = c.Int(nullable: false),
                        RegExpression = c.String(),
                        Host = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CrawlUrlSeeds",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NumericDataExtractorRules",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ExtractorExpression = c.String(),
                        Host = c.String(),
                        DataType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuthTokens", "ServiceCode", "dbo.ClinetServices");
            DropIndex("dbo.AuthTokens", new[] { "ServiceCode" });
            DropTable("dbo.NumericDataExtractorRules");
            DropTable("dbo.CrawlUrlSeeds");
            DropTable("dbo.CrawlRules");
            DropTable("dbo.CrawlHostSettings");
            DropTable("dbo.ClinetServices");
            DropTable("dbo.AuthTokens");
        }
    }
}
