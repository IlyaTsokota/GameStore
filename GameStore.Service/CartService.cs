using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using GameStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        private readonly IUnitOfWork _unitOfWork;

        public CartService(ICartRepository cartRepository, IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
        }

        public void AddItem(Product product, int quantity, string userId)
        {
            var cartLine = _cartRepository.Get(x => x.ProductId == product.ProductId);
            if (cartLine == null)
            {
                _cartRepository.Add(new CartLine { Product = product, Quantity = quantity, UserId = userId });
            }
            else
            {
                cartLine.Quantity += quantity;
                _cartRepository.Update(cartLine);
            }
            _unitOfWork.Commit();
        }

        public void Clear(string userId)
        {
            var cartLines = _cartRepository.GetMany(x => x.UserId == userId);
            foreach (var item in cartLines)
            {
                _cartRepository.Delete(item);
            }
            _unitOfWork.Commit();
        }

        public List<CartLine> GetCartLines(string userId)
        {
            var cartLines = _cartRepository.GetMany(x => x.UserId == userId);
            return cartLines.ToList();
        }

        public double GetTotalValue()
        {
            var total = _cartRepository.GetAll().Select(x => x.Product.Price * x.Quantity).Sum();
            return total;
        }

        public void RemoveLine(int id)
        {
            var cartLine = _cartRepository.GetById(id);
            _cartRepository.Delete(cartLine);
            _unitOfWork.Commit();
        }
        public CartLine Get(int id)
        {
            var cartLine = _cartRepository.GetById(id);
            return cartLine;
        }

        public void Update(CartLine cartLine)
        {
            _cartRepository.Update(cartLine);
            _unitOfWork.Commit();
        }

        public double GetLinePriceTotal(CartLine cartLine)
        {
            var linePriceTotal = cartLine.Quantity + cartLine.Product.Price;
            return linePriceTotal;
        }
    }
}
