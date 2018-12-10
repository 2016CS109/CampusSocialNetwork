namespace Campus_Social_Network.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postTable1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "postedIn", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "postedIn", c => c.Int(nullable: false));
        }
    }
}
