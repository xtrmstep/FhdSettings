namespace FhdSettings.Impl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
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
            DropTable("dbo.NumericDataExtractorRules");
            DropTable("dbo.CrawlUrlSeeds");
            DropTable("dbo.CrawlRules");
            DropTable("dbo.CrawlHostSettings");
        }
    }
}
