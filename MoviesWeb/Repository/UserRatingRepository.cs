namespace MoviesWeb.Repository
{
    public class UserRatingRepository<TEntity> : Repository<TEntity> where TEntity : BaseEntity
    {
        public UserRatingRepository(MoviesContext context) : base(context)
        {
        }
    }
}    