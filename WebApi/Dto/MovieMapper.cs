using ApplicationCore.Domain.Models;
using ApplicationCore.Domain.ValueObject;

namespace WebApi.Dto;

public class MovieMapper
{
    public static MovieDto From(Movie movie)
    {
        return new MovieDto()
        {
            Id = movie.Id,
            Title = movie.Title,
            ReleaseDate = movie.ReleaseDate,
            Description = movie.Description,
            Reviews = movie.Reviews.Select(r => MovieMapper.From(r)).ToList(),
            
        };
    }

    public static ReviewDto From(Review review)
    {
        return new ReviewDto()
        {
            UserId = review.UserIdGuid,
            Content = review.Content,
            Rate = review.Rate,
            Title = review.Title
        };
    }

    public static Review To(Guid movieId, NewReviewDto dto)
    {
        return new Review( movieId, dto.UserId, dto.Title, dto.Content, ReviewRate.Of(dto.Rate));
    }
}