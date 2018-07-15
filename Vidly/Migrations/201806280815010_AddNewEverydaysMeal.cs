namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewEverydaysMeal : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.EverydaysMeals", "MealEvent_Id", "dbo.MealEvents");
            DropIndex("dbo.EverydaysMeals", new[] { "MealEvent_Id" });
           // DropColumn("dbo.EverydaysMeals", "MealEvent_Id");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.EverydaysMeals", "MealEvent_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.EverydaysMeals", "MealEvent_Id");
            //AddForeignKey("dbo.EverydaysMeals", "MealEvent_Id", "dbo.MealEvents", "Id", cascadeDelete: true);
        }
    }
}
