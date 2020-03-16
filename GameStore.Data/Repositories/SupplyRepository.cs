
using GameStore.Data.Infrastructure;
using GameStore.Model;


namespace GameStore.Data.Repositories
{
    public class SupplyRepository : RepositoryBase<Supply>, ISupplyRepository
    {
        public SupplyRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
