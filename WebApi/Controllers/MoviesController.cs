using ApplicationCore.Application.Services;
using ApplicationCore.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController(IMovieServiceAsync movieServiceAsync) : ControllerBase
    {
        [HttpGet]
        [Produces("application/json")]
        public async Task<IQueryable<MovieDto>> GetAllMovies()
        {
            return (await movieServiceAsync.GetAllMoviesAsync()).Select(m => MovieMapper.From(m));
        }

        [HttpGet("id={id:guid}")]
        [Produces("application/json")]
        public async Task<ActionResult<MovieDto>> GetById(Guid id)
        {
            var movie = await movieServiceAsync.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(MovieMapper.From(movie));
        }

        [HttpPost("id={idMovie:guid}/reviews")]
        public async Task<CreatedResult> PostReview(Guid idMovie, NewReviewDto dto)
        {
            var review = MovieMapper.To(idMovie, dto);
            var movie = await movieServiceAsync.AddReviewToMovieAsync(review);
            return Created("", MovieMapper.From(movie));
        }
    }
}
