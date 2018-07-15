namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveEverydaysMeal : DbMigration
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
                        Breakfast = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Launch = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Dinner = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Member_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.EverydaysMeals", "Member_Id");
            AddForeignKey("dbo.EverydaysMeals", "Member_Id", "dbo.Members", "Id", cascadeDelete: true);
        }
    }
}
