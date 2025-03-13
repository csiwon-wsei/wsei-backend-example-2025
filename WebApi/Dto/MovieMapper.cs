using ApplicationCore.Domain.Models;
using ApplicationCore.Domain.ValueObject;

namespace WebApi.Dto;

public class MovieMapper
{
    public static MovieDto From(Movie movie)
    {
        return new MovieDto(movie.Title, movie.Description, movie.ReleaseDate)
        {
            Reviews = movie.Reviews.Select(r => MovieMapper.From(r)).ToList(),
            Id = movie.Id,
        };
    }

    public static ReviewDto From(Review review)
    {
        return new ReviewDto(review.UserIdGuid, review.Title, review.Content, review.Rate)
        {
            Id = review.Id,
        };
    }

    public static Review To(Guid movieId, NewReviewDto dto)
    {
        return new Review( movieId, dto.UserId, dto.Title, dto.Content, ReviewRate.Of(dto.Rate));
    }
}