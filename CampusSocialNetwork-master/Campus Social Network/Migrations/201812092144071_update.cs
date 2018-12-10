namespace Campus_Social_Network.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [firstName], [lastName], [dateOfBirth], [registrationNumber], [cnic], [classId]) VALUES (N'0f6d20b5-3d2b-4227-972e-af4e2e427080', N'admin@CSN.com', 0, N'AAqdfSJ1SDn6f6IKyzYiEgqF5Cc6VBCsHgo4PKaHuGbxm/wh3/2NbVBVuuyV8uyqhw==', N'353b64f3-0c48-4b9f-ad68-b5d311023299', N'11111111111', 0, 0, NULL, 1, 0, N'admin@CSN.com', N'Admin', N'Admin', N'1990-01-01 00:00:00', N'0000-CSN-000', N'1111111111111', 1007) 
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0f6d20b5-3d2b-4227-972e-af4e2e427080', N'328b7d0b-d583-4eeb-9634-e9e31576e906')");
             
            AddColumn("dbo.AspNetUsers", "firstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "lastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "dateOfBirth", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "registrationNumber", c => c.String());
            AddColumn("dbo.AspNetUsers", "cnic", c => c.String());
            AddColumn("dbo.AspNetUsers", "classId", c => c.Int(nullable: false));
            DropTable("dbo.Admins");
            DropTable("dbo.Students");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        registrationNumber = c.String(nullable: false, maxLength: 128),
                        emailId = c.String(nullable: false, maxLength: 128),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        dateOfBirth = c.DateTime(),
                        cnic = c.String(nullable: false, maxLength: 13),
                        contactNumber = c.String(maxLength: 11),
                        password = c.String(nullable: false),
                        classId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.registrationNumber, t.emailId });
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        emailId = c.String(nullable: false, maxLength: 128),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        contactNumber = c.String(maxLength: 11),
                        password = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.id, t.emailId });
            
            DropColumn("dbo.AspNetUsers", "classId");
            DropColumn("dbo.AspNetUsers", "cnic");
            DropColumn("dbo.AspNetUsers", "registrationNumber");
            DropColumn("dbo.AspNetUsers", "dateOfBirth");
            DropColumn("dbo.AspNetUsers", "lastName");
            DropColumn("dbo.AspNetUsers", "firstName");
        }
    }
}
