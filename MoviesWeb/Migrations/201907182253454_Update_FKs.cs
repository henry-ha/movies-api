namespace MoviesWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_FKs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRating", "Movie_Id1", "dbo.Movie");
            DropForeignKey("dbo.UserRating", "User_Id1", "dbo.User");
            DropIndex("dbo.UserRating", new[] { "Movie_Id1" });
            DropIndex("dbo.UserRating", new[] { "User_Id1" });
            DropColumn("dbo.UserRating", "Movie_Id");
            DropColumn("dbo.UserRating", "User_Id");
            RenameColumn(table: "dbo.UserRating", name: "Movie_Id1", newName: "Movie_Id");
            RenameColumn(table: "dbo.UserRating", name: "User_Id1", newName: "User_Id");
            AlterColumn("dbo.UserRating", "Movie_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.UserRating", "User_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.UserRating", "User_Id");
            CreateIndex("dbo.UserRating", "Movie_Id");
            AddForeignKey("dbo.UserRating", "Movie_Id", "dbo.Movie", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserRating", "User_Id", "dbo.User", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRating", "User_Id", "dbo.User");
            DropForeignKey("dbo.UserRating", "Movie_Id", "dbo.Movie");
            DropIndex("dbo.UserRating", new[] { "Movie_Id" });
            DropIndex("dbo.UserRating", new[] { "User_Id" });
            AlterColumn("dbo.UserRating", "User_Id", c => c.Int());
            AlterColumn("dbo.UserRating", "Movie_Id", c => c.Int());
            RenameColumn(table: "dbo.UserRating", name: "User_Id", newName: "User_Id1");
            RenameColumn(table: "dbo.UserRating", name: "Movie_Id", newName: "Movie_Id1");
            AddColumn("dbo.UserRating", "User_Id", c => c.Int(nullable: false));
            AddColumn("dbo.UserRating", "Movie_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.UserRating", "User_Id1");
            CreateIndex("dbo.UserRating", "Movie_Id1");
            AddForeignKey("dbo.UserRating", "User_Id1", "dbo.User", "Id");
            AddForeignKey("dbo.UserRating", "Movie_Id1", "dbo.Movie", "Id");
        }
    }
}
