
using GameStore.Data.Infrastructure;
using GameStore.Model;


namespace GameStore.Data.Repositories
{
    public class AttributeValueRepository : RepositoryBase<AttributeValue>, IAttributeValueRepository
    {
        public AttributeValueRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
