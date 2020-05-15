
using GameStore.Data.Infrastructure;
using GameStore.Model;

namespace GameStore.Data.Repositories
{
    public class AttributeRepository : RepositoryBase<Attribute>, IAttributeRepository
    {
        public AttributeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
