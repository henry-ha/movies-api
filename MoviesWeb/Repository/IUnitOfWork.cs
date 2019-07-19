using System;

namespace MoviesWeb.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IUnitOfWork BeginTransaction();
        IUnitOfWork SaveAndContinue();
        bool EndTransaction();
        void RollBack();
        int Complete();
    }
}