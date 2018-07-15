namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgainPopulateMembershipTypeWithName : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET MembershipTypeName = 'Pay as you go' WHERE ID= 1");
            Sql("UPDATE MembershipTypes SET MembershipTypeName = 'Monthly' WHERE ID= 2");
            Sql("UPDATE MembershipTypes SET MembershipTypeName = 'Quarterly' WHERE ID= 3");
            Sql("UPDATE MembershipTypes SET MembershipTypeName = 'Yearly' WHERE ID= 4");

        }

        public override void Down()
        {
        }
    }
}
