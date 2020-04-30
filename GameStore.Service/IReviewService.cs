using GameStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public interface IReviewService
    {
        Review GetReview(int id);

        List<Review> GetReviews(int productId);

        void CreateReview(Review review);

        void DeleteReview(Review review);
    }
}
