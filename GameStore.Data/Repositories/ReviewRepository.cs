
using GameStore.Data.Infrastructure;
using GameStore.Model;


namespace GameStore.Data.Repositories
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
