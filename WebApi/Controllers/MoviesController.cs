using System.Net.Mime;
using ApplicationCore.Application.Exceptions;
using ApplicationCore.Application.Services;
using ApplicationCore.Domain.Models;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using WebApi.Dto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController(IMovieServiceAsync movieServiceAsync) : ControllerBase
    {
        [AcceptVerbs("Get")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async IAsyncEnumerable<MovieDto> GetAllMovies()
        {
            var movies = await movieServiceAsync.GetAllMoviesAsync();
            foreach (var movie in movies)
            {
                yield return MovieMapper.From(movie);
            }
        }

        [HttpGet("id={id:guid}")]
        [Produces("application/json")]
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

        [HttpPost("id={idMovie:guid}/reviews")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PostReview(Guid idMovie, NewReviewDto dto)
        {
            var review = MovieMapper.To(idMovie, dto);
            try
            {
                var movie = await movieServiceAsync.AddReviewToMovieAsync(review);
                return Created("", MovieMapper.From(movie));
            }
            catch (UserNotFoundException e)
            {
                return BadRequest(new
                {
                    error = e.Message
                });
            }
        }
        
        [HttpGet("id={idMovie:guid}/reviews/{idReview:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Review>> GetReviewsForMovie(Guid idMovie, Guid idReview)
        {
            var movie = await movieServiceAsync.GetMovieByIdAsync(idMovie);
            var review = movie?.Reviews.FirstOrDefault(r => r.Id == idReview);
            return review == null ? NotFound() : Ok(review);
        }
        
        [HttpGet("id={idMovie:guid}/reviews")]
        [Produces(MediaTypeNames.Application.Json, 
            MediaTypeNames.Application.Xml)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllReviewsForMovie(Guid idMovie)
        {
            var movie = await movieServiceAsync.GetMovieByIdAsync(idMovie);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie?.Reviews.Select(r => MovieMapper.From(r)).ToList());
        }
    }
}