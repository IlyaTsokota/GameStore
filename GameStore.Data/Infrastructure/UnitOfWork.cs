using System;

namespace GameStore.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory _databaseFactory;

        private bool _disposed;

        private ApplicationContext.ApplicationContext _dbContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        protected ApplicationContext.ApplicationContext DbContext => _dbContext ?? (_dbContext = _databaseFactory.Get());

        public void Commit()
        {
            DbContext.Commit();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
