namespace MoviesWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_MovieDuration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movie", "Genres", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Movie", "Duration", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Movie", "Genre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movie", "Genre", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Movie", "Duration");
            DropColumn("dbo.Movie", "Genres");
        }
    }
}
