using ApplicationCore.Domain.Models;

namespace UnitTests;

public class BaseIdentityTest
{
    [Fact]
    public void ShouldGuidBeCreatedAndNotNull()
    {
        //Arrage
        Movie movie = new Movie("test", "test", DateTime.Now)
        {
            Id = Guid.NewGuid(),
        }; 
        
        //Act
        
        //Assert
        Assert.NotNull(movie.Id);
    }
}