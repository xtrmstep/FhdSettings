namespace SettingsService.Impl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExtractRules",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        DataType = c.Int(nullable: false),
                        RegExpression = c.String(),
                        Host_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hosts", t => t.Host_Id)
                .Index(t => t.Host_Id);
            
            CreateTable(
                "dbo.Hosts",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SeedUrl = c.String(),
                        AddedToSeed = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HostSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Disallow = c.String(),
                        CrawlDelay = c.Int(nullable: false),
                        Host_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hosts", t => t.Host_Id)
                .Index(t => t.Host_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HostSettings", "Host_Id", "dbo.Hosts");
            DropForeignKey("dbo.ExtractRules", "Host_Id", "dbo.Hosts");
            DropIndex("dbo.HostSettings", new[] { "Host_Id" });
            DropIndex("dbo.ExtractRules", new[] { "Host_Id" });
            DropTable("dbo.HostSettings");
            DropTable("dbo.Hosts");
            DropTable("dbo.ExtractRules");
        }
    }
}
