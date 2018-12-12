namespace Campus_Social_Network.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateInUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "imagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "imagePath");
        }
    }
}
