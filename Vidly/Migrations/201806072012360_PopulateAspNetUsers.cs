namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateAspNetUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                    INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'd3fbc1e2-e6ce-40f4-a6b7-3d3762ed2f39', N'CanManageMovies')
                    INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'731ee8f0-81bb-4e74-bcee-38da1711e197', N'khalidbinarif@outlook.com', 0, N'APK3Iq69zf+Lt2lbXY6+LTB+mfHEH34/v7yEDfqVPKD2SSsmKbnZY4JcZzySDGbVhA==', N'cdd547bf-0598-4b5a-ac2a-c749063d3d43', NULL, 0, 0, NULL, 1, 0, N'khalidbinarif@outlook.com')
                    INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'bc1b1593-2be7-4188-a0a3-00e595b76084', N'admin@vidly.com', 0, N'AL2/Af1DgqNHOE69HiJc8pusi0OS1z2AKrvy54A6L/R041CtbxMw/xVHl8N7nhs9jw==', N'b95b5459-fcdb-46ee-acf4-4405878c7d88', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                    INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bc1b1593-2be7-4188-a0a3-00e595b76084', N'd3fbc1e2-e6ce-40f4-a6b7-3d3762ed2f39')   
               ");

        }

    public override void Down()
        {
        }
    }
}
