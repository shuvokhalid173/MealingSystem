namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMealEventAndMember : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MealEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventName = c.String(nullable: false, maxLength: 60),
                        Email = c.String(nullable: false),
                        NumberOfMember = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        MealEvent_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MealEvents", t => t.MealEvent_Id, cascadeDelete: true)
                .Index(t => t.MealEvent_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "MealEvent_Id", "dbo.MealEvents");
            DropIndex("dbo.Members", new[] { "MealEvent_Id" });
            DropTable("dbo.Members");
            DropTable("dbo.MealEvents");
        }
    }
}
