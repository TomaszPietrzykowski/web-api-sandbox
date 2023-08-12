using WebApiSandbox.Models;

namespace WebApiSandbox.Interfaces
{
    public interface IReviewRepository
    {
        Review GetReview(int reviewId);
        ICollection<Review> GetProductReviews(int productId);
        ICollection<Review> GetAllReviews();
        bool ReviewExists(int reviewId);
    }
}
