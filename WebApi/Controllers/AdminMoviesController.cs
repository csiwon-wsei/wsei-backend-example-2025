using System.Net.Mime;
using ApplicationCore.Application.Commons;
using ApplicationCore.Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers
{
    [Route("api/admin/movies")]
    [ApiController]
    public class AdminMoviesController(
        ILogger<AdminMoviesController> logger,
        IGenericRepositoryAsync<Movie> movies,
        IGenericRepositoryAsync<User> user
    ) : ControllerBase
    {
        // GET: api/<AdminMoviesController>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async IAsyncEnumerable<MovieDto> GetAll()
        {
            foreach (var movie in await movies.GetAllAsync())
            {
                yield return MovieMapper.From(movie);
            }
        }
        
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<Movie>> Get(Guid id)
        {
            var r = (await movies.GetAllAsync()).FirstOrDefault(m => m.Id == id, null);
            return r == null ? NotFound() : Ok(MovieMapper.From(r));
        }

        // POST api/<AdminMoviesController>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        public async Task<ActionResult> Post(NewMovieDto dto)
        {
            var m = await movies.AddAsync(MovieMapper.To(dto));
            m.ReleaseDate = DateTime.Now;
            m.Reviews = new();
            return CreatedAtAction(nameof(Get), new { Id = m.Id }, MovieMapper.From(m));
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, NewMovieDto dto)
        {
            var m = await movies.GetByIdAsync(id);
            if (m is null)
            {
                return NotFound();
            }

            var movie = MovieMapper.To(dto);
            movie.Id = id;
            movie.Reviews = m.Reviews;
            var updateAsync = await movies.UpdateAsync(movie);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await movies.DeleteByIdAsync(id))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        
        [HttpPatch("{id:guid}")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.JsonPatch)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddReview(Guid id, [FromBody] JsonPatchDocument<Movie> document)
        {
            foreach (var op in document.Operations)
            {
                logger.LogDebug(op.op);
                logger.LogDebug(op.value.ToString());
                logger.LogDebug(op.path);
            }
            var movie = (await movies.GetByIdAsync(id));
            if (movie == null)
            {
                return BadRequest();
            }
            document.ApplyTo(movie, ModelState);
            logger.LogInformation($"Patched title in movie {movie.Title}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await movies.UpdateAsync(movie);
            return NoContent();
        }
        
        [HttpPatch("{id:guid}/reviews")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.JsonPatch)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RateReview(Guid id, Guid reviewId, [FromBody] JsonPatchDocument<Movie> document)
        {
            var movie = (await movies.GetByIdAsync(id));
            if (movie == null)
            {
                return BadRequest();
            }
            document.ApplyTo(movie, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await movies.UpdateAsync(movie);
            return NoContent();
        }
    }
}