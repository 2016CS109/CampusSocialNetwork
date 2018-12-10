namespace Campus_Social_Network.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialModel1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        lastName = c.String(),
                        contactNumber = c.String(),
                        emailId = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        lastName = c.String(),
                        dateOfBirth = c.DateTime(nullable: false),
                        registrationNumber = c.String(),
                        cnic = c.String(),
                        contactNumber = c.String(),
                        emailId = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        lastName = c.String(),
                        contactNumber = c.String(),
                        emailId = c.String(),
                        designation = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Classes");
            DropTable("dbo.Admins");
        }
    }
}
