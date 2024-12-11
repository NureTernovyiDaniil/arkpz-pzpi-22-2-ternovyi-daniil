using ChefMate_backend.Models;
using ChefMate_backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ChefMate_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var reviews = await _reviewRepository.Retrieve();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var review = await _reviewRepository.Retrieve(id);
            return Ok(review);
        }

        [HttpPost("post")]
        public async Task<IActionResult> Post(ReviewDto review)
        {
            await _reviewRepository.Insert(review);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(ReviewDto review)
        {
            await _reviewRepository.Update(review);
            return Ok();
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var review = await _reviewRepository.Retrieve(id);
            await _reviewRepository.Delete(review);
            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(ReviewDto review)
        {
            await _reviewRepository.Delete(review);
            return Ok();
        }
    }
}
