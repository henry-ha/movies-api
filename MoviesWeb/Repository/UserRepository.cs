namespace MoviesWeb.Repository
{
    public class UserRepository<TEntity> : Repository<TEntity> where TEntity : BaseEntity
    {
        public UserRepository(MoviesContext context) : base(context)
        {
        }
    }
}    