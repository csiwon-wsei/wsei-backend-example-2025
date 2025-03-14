using ApplicationCore.Application.Exceptions;
using ApplicationCore.Application.Services;
using ApplicationCore.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
using WebApi.Handlers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController(IMovieServiceAsync movieServiceAsync, ILogger<MoviesController> logger)
        : ControllerBase
    {
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IQueryable<MovieDto>> GetAllMovies()
        {
            return (await movieServiceAsync.GetAllMoviesAsync()).Select(m => MovieMapper.From(m));
        }

        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieDto>> GetById(Guid id)
        {
            var movie = await movieServiceAsync.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(MovieMapper.From(movie));
        }
        [HttpGet("{movieId:guid}/reviews/{reviewId:guid}")]
        public async Task<ActionResult<Review>> GetReviewById(Guid movieId, Guid reviewId)
        {
            var review = (await movieServiceAsync.GetMovieByIdAsync(movieId))?.Reviews.FirstOrDefault(r => r.Id == reviewId);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        [HttpPost("{movieId:guid}/reviews")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [MovieExceptionFilter]
        public async Task<IActionResult> PostReview(Guid movieId, NewReviewDto dto)
        {
            var review = MovieMapper.To(movieId, dto);
            review.Id = Guid.NewGuid();
            var movie = await movieServiceAsync.AddReviewToMovieAsync(review);
            return CreatedAtAction(nameof(GetReviewById), new {movieId = movie.Id, reviewId = review.Id }, MovieMapper.From(movie));
        }

        [HttpPost("{movieId:guid}/result/reviews")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [MovieExceptionFilter]
        public async Task<IActionResult> PostReviewResult(Guid movieId, NewReviewDto dto)
        {
            var review = MovieMapper.To(movieId, dto);
            review.Id = new Guid();
            var result = await movieServiceAsync.AddReviewToMovieResultAsync(review);
            if (result.IsError)
            {
                return BadRequest(
                    new
                    {
                        error = result.Error,
                    });
            }

            var movie = result.Value;
            return CreatedAtAction(nameof(GetReviewById), new {movieId = movie.Id, reviewId = review.Id}, MovieMapper.From(movie));
        }
    }
}