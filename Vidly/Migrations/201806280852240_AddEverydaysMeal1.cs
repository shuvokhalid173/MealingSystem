namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEverydaysMeal1 : DbMigration
    {
        public override void Up()
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
