namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDrivingLicenceToApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DrivingLicence", c => c.String(nullable: false, maxLength: 6));
            DropColumn("dbo.AspNetUsers", "Licence");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Licence", c => c.String(nullable: false, maxLength: 6));
            DropColumn("dbo.AspNetUsers", "DrivingLicence");
        }
    }
}
