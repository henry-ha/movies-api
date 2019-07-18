using MoviesWeb.Models;
using System.Data.Entity;

public abstract class BaseContext : DbContext
{
    public BaseContext()
    {
    }

    protected BaseContext(string connString)
        : base(connString)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRating> UserRatings { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
