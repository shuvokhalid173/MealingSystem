namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembershipType : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MembershipTypes (ID, SignUpFee, DurationInMonth, DiscountRate) VALUES (1, 0, 0, 0)"); // pay as you go
            Sql("INSERT INTO MembershipTypes (ID, SignUpFee, DurationInMonth, DiscountRate) VALUES (2, 30, 1, 10)"); // Monthly
            Sql("INSERT INTO MembershipTypes (ID, SignUpFee, DurationInMonth, DiscountRate) VALUES (3, 90, 3, 15)"); // quaterly
            Sql("INSERT INTO MembershipTypes (ID, SignUpFee, DurationInMonth, DiscountRate) VALUES (4, 300, 12, 20)"); // yearly
        }
        
        public override void Down()
        {
        }
    }
}
