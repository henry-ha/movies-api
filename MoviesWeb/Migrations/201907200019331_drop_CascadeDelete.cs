namespace MoviesWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class drop_CascadeDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRating", "Movie_Id", "dbo.Movie");
            DropForeignKey("dbo.UserRating", "User_Id", "dbo.User");
            AddForeignKey("dbo.UserRating", "Movie_Id", "dbo.Movie", "Id");
            AddForeignKey("dbo.UserRating", "User_Id", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRating", "User_Id", "dbo.User");
            DropForeignKey("dbo.UserRating", "Movie_Id", "dbo.Movie");
            AddForeignKey("dbo.UserRating", "User_Id", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserRating", "Movie_Id", "dbo.Movie", "Id", cascadeDelete: true);
        }
    }
}
