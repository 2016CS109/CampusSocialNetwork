namespace Campus_Social_Network.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        postText = c.String(nullable: false),
                        isPublic = c.Boolean(nullable: false),
                        likes = c.Int(nullable: false),
                        postedBy = c.String(nullable: false),
                        postedIn = c.Int(),
                        postTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Posts");
        }
    }
}
