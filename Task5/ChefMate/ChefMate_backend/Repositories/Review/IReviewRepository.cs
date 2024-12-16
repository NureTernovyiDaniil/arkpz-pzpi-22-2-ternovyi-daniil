using ChefMate_backend.Models;

namespace ChefMate_backend.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> Retrieve();
        Task<Review> Retrieve(Guid reviewId);
        Task<bool> Insert(ReviewDto review);
        Task<bool> Update(ReviewDto review);
        Task<bool> Delete(Guid reviewId);
        Task<bool> Delete(ReviewDto review);
    }
}