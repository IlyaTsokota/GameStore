
using GameStore.Data.Infrastructure;

using Attribute = System.Attribute;

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
