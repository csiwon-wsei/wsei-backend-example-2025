using ApplicationCore.Application.Commons;
using ApplicationCore.Domain.Models;
using ApplicationCore.Domain.ValueObject;

namespace ApplicationCore.Application.Services;

public interface IMovieServiceAsync
{
    Task<IQueryable<Movie>> GetAllMoviesAsync();
    Task<Movie?> GetMovieByIdAsync(Guid id);
    Task<Movie> AddReviewToMovieAsync(Review review);
    Task<Result<Movie>> AddReviewToMovieResultAsync(Review review);
}