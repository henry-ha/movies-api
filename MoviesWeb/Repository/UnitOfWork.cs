using MoviesWeb.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace MoviesWeb.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MoviesContext _context;
        private DbContextTransaction _transaction { get; set; }

        public virtual IRepository<Movie> MovieRepository { get; private set; }
        public virtual IRepository<User> UserRepository { get; private set; }
        public virtual IRepository<UserRating> UserRatingRepository { get; private set; }

        public UnitOfWork()
        {
            _context = new MoviesContext();
            _context.Configuration.AutoDetectChangesEnabled = true;

            MovieRepository = new MovieRepository<Movie>(_context);
            UserRepository = new UserRepository<User>(_context);
            UserRatingRepository = new UserRatingRepository<UserRating>(_context);
        }

        public IUnitOfWork BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
            return this;
        }

        public IUnitOfWork SaveAndContinue()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw dbEx;
            }
            return this;
        }

        public bool EndTransaction()
        {
            try
            {
                _context.SaveChanges();
                _transaction.Commit();
                Dispose();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw dbEx;
            }
            return true;
        }

        public void RollBack()
        {
            _transaction.Rollback();
            Dispose();

        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}