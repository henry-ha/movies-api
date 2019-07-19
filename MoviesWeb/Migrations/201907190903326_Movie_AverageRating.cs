namespace MoviesWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Movie_AverageRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movie", "AverageRating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movie", "AverageRating");
        }
    }
}
