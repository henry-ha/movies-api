using MoviesWeb.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

public abstract class BaseContext : DbContext
{
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
        modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
    }
}
