using GameStore.Model;

using System.Collections.Generic;


namespace GameStore.Service
{
    public interface ICartService
    {
        void AddItem(Product product, int quantity, string userId);

        void RemoveLine(int id);

        double GetTotalValue();

        void Clear(string userId);

        List<CartLine> GetCartLines(string userId);

        CartLine Get(int id);

        void Update(CartLine cartLine);

        double GetLinePriceTotal(CartLine cartLine);
    }
}
