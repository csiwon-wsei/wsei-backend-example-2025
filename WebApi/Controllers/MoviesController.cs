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

        [HttpPost("{movieId:guid}/reviews")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [MovieExceptionFilter]
        public async Task<IActionResult> PostReview(Guid movieId, NewReviewDto dto)
        {
            var review = MovieMapper.To(movieId, dto);
            var movie = await movieServiceAsync.AddReviewToMovieAsync(review);
            return Created("", MovieMapper.From(movie));
        }

        [HttpPost("{movieId:guid}/result/reviews")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [MovieExceptionFilter]
        public async Task<IActionResult> PostReviewResult(Guid movieId, NewReviewDto dto)
        {
            var review = MovieMapper.To(movieId, dto);
            var movie = await movieServiceAsync.AddReviewToMovieResultAsync(review);
            if (movie.IsError)
            {
                return BadRequest(
                    new
                    {
                        error = movie.Error,
                    });
            }
            return Created("", MovieMapper.From(movie.Value));
        }
    }
}