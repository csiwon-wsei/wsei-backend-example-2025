using ApplicationCore.Application.Commons;
using ApplicationCore.Domain.Models;
using Infrastructer.Memory;
using Infrastructure.Memory;

namespace UnitTests;

public class MemoryGenericRepositoryAsyncTests
{
    [Fact]
    public async Task ShouldAddMovieReturnMovieWidthId()
    {
        // Arrange
        IGenericRepositoryAsync<Movie> movies = new MemoryGenericRepositoryAsync<Movie>();

        Movie movie = new Movie("test", "test", DateTime.Now);
        Guid prevId = movie.Id;
        //Act
        var result = await movies.AddAsync(movie);
        
        //Assert
        Assert.NotEqual(prevId, result.Id);
    }
}