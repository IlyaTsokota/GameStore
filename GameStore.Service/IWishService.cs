using GameStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public interface IWishService
    {
        List<Wish> GetWishes(string userId);

        void AddWishList(Product product, string userId);

        void Delete(Wish wish);

        Wish GetWish(int id);

        void Clear(string userId);
    }
}
