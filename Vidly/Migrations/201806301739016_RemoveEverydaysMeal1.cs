namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveEverydaysMeal1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EverydaysMeals", "Member_Id", "dbo.Members");
            DropIndex("dbo.EverydaysMeals", new[] { "Member_Id" });
            DropTable("dbo.EverydaysMeals");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.EverydaysMeals", "Member_Id");
            AddForeignKey("dbo.EverydaysMeals", "Member_Id", "dbo.Members", "Id", cascadeDelete: true);
        }
    }
}
