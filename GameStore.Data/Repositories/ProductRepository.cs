
using GameStore.Data.Infrastructure;
using GameStore.Model;


namespace GameStore.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
