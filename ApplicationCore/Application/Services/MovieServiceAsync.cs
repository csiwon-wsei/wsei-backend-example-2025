using ApplicationCore.Application.Commons;
using ApplicationCore.Application.Exceptions;
using ApplicationCore.Domain.Models;

namespace ApplicationCore.Application.Services;

public class MovieServiceAsync: IMovieServiceAsync
{
    private readonly IGenericRepositoryAsync<Movie> _movies;
    private readonly IGenericRepositoryAsync<User> _users;

    public MovieServiceAsync(IGenericRepositoryAsync<Movie> movies, IGenericRepositoryAsync<User> users)
    {
        _movies = movies;
        _users = users;
    }

    public async Task<IQueryable<Movie>> GetAllMoviesAsync()
    {
        return await _movies.GetAllAsync();
    }

    public async Task<Movie?> GetMovieByIdAsync(Guid id)
    {
        return await _movies.GetByIdAsync(id);
    }

    public async Task<Movie> AddReviewToMovieAsync(Review review)
    {
        review.Id = Guid.NewGuid();
        var movie = await _movies.GetByIdAsync(review.MovieIdGuid);
        if (movie == null)
        {
            throw new MovieNotFoundException("Movie not found!");
        }
        var user = await _users.GetByIdAsync(review.UserIdGuid);
        if (user == null)
        {
            throw new UserNotFoundException("User not found!");
        }
        movie.Reviews.Add(review);
        await _movies.SaveChangesAsync();
        return movie;
        
    }
}