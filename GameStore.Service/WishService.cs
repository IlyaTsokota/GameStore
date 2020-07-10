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
    public class WishService : IWishService
    {
        private readonly IWishRepository _wishRepository;

        private readonly IUnitOfWork _unitOfWork;

        public WishService(IWishRepository wishRepository, IUnitOfWork unitOfWork)
        {
            _wishRepository = wishRepository;

            _unitOfWork = unitOfWork;
        }

        public void AddWishList(Product product, string userId, out int result)
        {
            var wish = _wishRepository.Get(x => x.ProductId == product.ProductId && x.UserId == userId);
            if (wish == null)
            {
                _wishRepository.Add(new Wish { Product = product, UserId = userId });
                _unitOfWork.Commit();
                result = 0;
            }
            else
            {
                result = 1;
            }

        }

        public void Clear(string userId)
        {
            var wishs = _wishRepository.GetMany(x => x.UserId == userId);
            foreach (var item in wishs)
            {
                _wishRepository.Delete(item);
            }
            _unitOfWork.Commit();
        }

        public void Delete(Wish wish)
        {
            _wishRepository.Delete(wish);
            _unitOfWork.Commit();
        }

        public Wish GetWish(int id)
        {
            var wish = _wishRepository.GetById(id);
            return wish;
        }

        public List<Wish> GetWishes(string userId)
        {
            var wishs = _wishRepository.GetMany(x => x.UserId == userId).ToList();
            return wishs;
        }
    }
}
