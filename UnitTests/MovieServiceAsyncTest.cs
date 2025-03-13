using ApplicationCore.Application.Commons;
using ApplicationCore.Application.Services;
using ApplicationCore.Domain.Models;
using ApplicationCore.Domain.ValueObject;
using Infrastructer.Memory;

namespace UnitTests;

public class MovieServiceAsyncTest
{
    private readonly IGenericRepositoryAsync<Movie> _movies; 
    private readonly IGenericRepositoryAsync<User> _users; 
    private IMovieServiceAsync _service; 
    
    public MovieServiceAsyncTest()
    {
        _movies = new MemoryGenericRepositoryAsync<Movie>();
        _users = new MemoryGenericRepositoryAsync<User>();
        _service = new MovieServiceAsync(_movies, _users);
        InitData.Init(_movies, _users);

    }

    [Fact]
    public async Task ShouldReturnMovieWithNewReview()
    {
        // Arrange
        var movie = (await _service.GetAllMoviesAsync()).FirstOrDefault();
        var user = (await _users.GetAllAsync()).FirstOrDefault();
        if (user == null || movie == null)
        {
            Assert.Fail();
        }
        var review = new Review(movie.Id, user.Id, "Test", "Test content", ReviewRate.Of(5));
        var prevCount = movie.Reviews.Count();
        
        // Act
        var movieWithReview = await _service.AddReviewToMovieAsync(review);
        
        // Assert
        Assert.Equal(prevCount + 1, movie.Reviews.Count());
        Assert.Contains(movieWithReview.Reviews, c => c.Title == "Test");
        Assert.Contains(movieWithReview.Reviews, c => c.Content == "Test content");
        Assert.Contains(movieWithReview.Reviews, c => c.Rate.Value == 5);
        
    }
}