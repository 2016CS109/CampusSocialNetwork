namespace Campus_Social_Network.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "classId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "classId");
        }
    }
}
