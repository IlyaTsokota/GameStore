
using GameStore.Data.Infrastructure;
using GameStore.Model;


namespace GameStore.Data.Repositories
{
    public class SupplyProductRepository : RepositoryBase<SupplyProduct>, ISupplyProductRepository
    {
        public SupplyProductRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
