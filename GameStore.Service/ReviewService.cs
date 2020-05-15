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
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IReviewRepository reviewRepository, IUnitOfWork unitOfWork)
        {
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
        }

        public Review GetReview(int id)
        {
            var review = _reviewRepository.GetById(id);
            return review;
        }

        public List<Review> GetReviews(int productId)
        {
            var reviews = _reviewRepository.GetMany(x => x.ProductId == productId).ToList();
            return reviews;
        }

        public void CreateReview(Review review)
        {
            _reviewRepository.Add(review);
            _unitOfWork.Commit();
        }

        public void DeleteReview(Review review)
        {
            _reviewRepository.Delete(review);
            _unitOfWork.Commit();
        }

    }
}
