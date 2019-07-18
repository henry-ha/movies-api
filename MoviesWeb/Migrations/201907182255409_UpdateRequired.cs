namespace MoviesWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movie", "Title", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Movie", "Genre", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.User", "Name", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "Name", c => c.String(maxLength: 255));
            AlterColumn("dbo.Movie", "Genre", c => c.String(maxLength: 255));
            AlterColumn("dbo.Movie", "Title", c => c.String(maxLength: 255));
        }
    }
}
