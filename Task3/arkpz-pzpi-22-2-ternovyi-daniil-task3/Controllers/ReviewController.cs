using ChefMate_backend.Models;
using ChefMate_backend.Repositories;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var reviews = await _reviewRepository.Retrieve();
            return Ok(reviews);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var review = await _reviewRepository.Retrieve(id);
            return Ok(review);
        }

        [Authorize]
        [HttpPost("post")]
        public async Task<IActionResult> Post(ReviewDto review)
        {
            var result = await _reviewRepository.Insert(review);
            if(result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update(ReviewDto review)
        {
            var result = await _reviewRepository.Update(review);
            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _reviewRepository.Delete(id);
            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(ReviewDto review)
        {
            var result = await _reviewRepository.Delete(review);
            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}