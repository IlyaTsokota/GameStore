using GameStore.Data.Infrastructure;
using GameStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Data.Repositories
{
    public class WishRepository : RepositoryBase<Wish>, IWishRepository
    {
        public WishRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
