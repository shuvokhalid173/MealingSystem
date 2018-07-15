namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyEverydaysMeal1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EverydaysMeals", "Breakfast", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.EverydaysMeals", "Launch", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.EverydaysMeals", "Dinner", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EverydaysMeals", "Dinner", c => c.Int(nullable: false));
            AlterColumn("dbo.EverydaysMeals", "Launch", c => c.Int(nullable: false));
            AlterColumn("dbo.EverydaysMeals", "Breakfast", c => c.Int(nullable: false));
        }
    }
}
