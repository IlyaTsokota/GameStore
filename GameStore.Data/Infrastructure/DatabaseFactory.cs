

namespace GameStore.Data.Infrastructure
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private ApplicationContext.ApplicationContext _dbContext;

        public ApplicationContext.ApplicationContext Get() =>
            _dbContext ?? (_dbContext = new ApplicationContext.ApplicationContext());
    }
}
