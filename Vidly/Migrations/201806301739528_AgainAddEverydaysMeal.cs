namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgainAddEverydaysMeal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EverydaysMeals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Breakfast = c.Int(nullable: false),
                        Launch = c.Int(nullable: false),
                        Dinner = c.Int(nullable: false),
                        Member_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.Member_Id, cascadeDelete: true)
                .Index(t => t.Member_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EverydaysMeals", "Member_Id", "dbo.Members");
            DropIndex("dbo.EverydaysMeals", new[] { "Member_Id" });
            DropTable("dbo.EverydaysMeals");
        }
    }
}
