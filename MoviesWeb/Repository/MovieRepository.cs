namespace MoviesWeb.Repository
{
    public class MovieRepository<TEntity> : Repository<TEntity> where TEntity : BaseEntity
    {
        public MovieRepository(MoviesContext context) : base(context)
        {
        }
    }
}    