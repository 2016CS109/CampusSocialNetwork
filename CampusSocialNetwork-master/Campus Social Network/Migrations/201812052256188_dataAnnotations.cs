namespace Campus_Social_Network.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataAnnotations : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Admins");
            DropPrimaryKey("dbo.Classes");
            DropPrimaryKey("dbo.Students");
            DropPrimaryKey("dbo.Teachers");
            AlterColumn("dbo.Admins", "firstName", c => c.String(nullable: false));
            AlterColumn("dbo.Admins", "lastName", c => c.String(nullable: false));
            AlterColumn("dbo.Admins", "contactNumber", c => c.String(maxLength: 11));
            AlterColumn("dbo.Admins", "emailId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Admins", "password", c => c.String(nullable: false));
            AlterColumn("dbo.Classes", "name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Students", "firstName", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "lastName", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "dateOfBirth", c => c.DateTime());
            AlterColumn("dbo.Students", "registrationNumber", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Students", "cnic", c => c.String(nullable: false, maxLength: 13));
            AlterColumn("dbo.Students", "contactNumber", c => c.String(maxLength: 11));
            AlterColumn("dbo.Students", "emailId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Students", "password", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "firstName", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "lastName", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "contactNumber", c => c.String(maxLength: 11));
            AlterColumn("dbo.Teachers", "emailId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Teachers", "designation", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Admins", new[] { "id", "emailId" });
            AddPrimaryKey("dbo.Classes", new[] { "id", "name" });
            AddPrimaryKey("dbo.Students", new[] { "id", "registrationNumber", "emailId" });
            AddPrimaryKey("dbo.Teachers", new[] { "id", "emailId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Teachers");
            DropPrimaryKey("dbo.Students");
            DropPrimaryKey("dbo.Classes");
            DropPrimaryKey("dbo.Admins");
            AlterColumn("dbo.Teachers", "designation", c => c.String());
            AlterColumn("dbo.Teachers", "emailId", c => c.String());
            AlterColumn("dbo.Teachers", "contactNumber", c => c.String());
            AlterColumn("dbo.Teachers", "lastName", c => c.String());
            AlterColumn("dbo.Teachers", "firstName", c => c.String());
            AlterColumn("dbo.Students", "password", c => c.String());
            AlterColumn("dbo.Students", "emailId", c => c.String());
            AlterColumn("dbo.Students", "contactNumber", c => c.String());
            AlterColumn("dbo.Students", "cnic", c => c.String());
            AlterColumn("dbo.Students", "registrationNumber", c => c.String());
            AlterColumn("dbo.Students", "dateOfBirth", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Students", "lastName", c => c.String());
            AlterColumn("dbo.Students", "firstName", c => c.String());
            AlterColumn("dbo.Classes", "name", c => c.String());
            AlterColumn("dbo.Admins", "password", c => c.String());
            AlterColumn("dbo.Admins", "emailId", c => c.String());
            AlterColumn("dbo.Admins", "contactNumber", c => c.String());
            AlterColumn("dbo.Admins", "lastName", c => c.String());
            AlterColumn("dbo.Admins", "firstName", c => c.String());
            AddPrimaryKey("dbo.Teachers", "id");
            AddPrimaryKey("dbo.Students", "id");
            AddPrimaryKey("dbo.Classes", "id");
            AddPrimaryKey("dbo.Admins", "id");
        }
    }
}
