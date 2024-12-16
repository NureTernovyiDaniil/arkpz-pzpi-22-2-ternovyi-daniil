using AutoMapper;
using ChefMate_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefMate_backend.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReviewRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _context = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<bool> Insert(ReviewDto reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);
            _context.Reviews.Add(review);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Review>> Retrieve()
        {
            var reviews = await _context.Reviews.ToListAsync();
            return reviews;
        }

        public async Task<Review> Retrieve(Guid reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            return _mapper.Map<Review>(review);
        }

        public async Task<bool> Update(ReviewDto reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);
            _context.Reviews.Update(review);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null)
            {
                return false;
            }

            _context.Reviews.Remove(review);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(ReviewDto reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);
            if (review == null)
            {
                throw new KeyNotFoundException("Review not found.");
            }

            _context.Reviews.Remove(review);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
