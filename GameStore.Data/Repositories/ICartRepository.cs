using GameStore.Data.Infrastructure;
using GameStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Data.Repositories
{
    public interface ICartRepository : IRepository<CartLine>
    {
    }
}
