
using GameStore.Data.Infrastructure;
using GameStore.Model;


namespace GameStore.Data.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
