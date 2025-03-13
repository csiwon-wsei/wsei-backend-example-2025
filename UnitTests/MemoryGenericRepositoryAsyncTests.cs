using ApplicationCore.Application.Commons;
using ApplicationCore.Application.Services;
using ApplicationCore.Domain.Models;
using Infrastructer.Memory;

namespace UnitTests;

public class MemoryGenericRepositoryAsyncTests
{
    [Fact]
    public async void ShouldAddMovieReturnMovieWidthId()
    {
        // Arrange
        IGenericRepositoryAsync<Movie> _movies = new MemoryGenericRepositoryAsync<Movie>();

        Movie movie = new Movie("test", "test", DateTime.Now);
        Guid prevId = movie.Id;
        //Act
        var result = await _movies.AddAsync(movie);
        
        //Assert
        Assert.NotEqual(prevId, result.Id);
    }
}