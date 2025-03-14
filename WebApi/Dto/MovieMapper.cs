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
            ReleaseDate = movie.ReleaseDate,
            Description = movie.Description,
            Title = movie.Title,
            Reviews = movie.Reviews.Select(r => MovieMapper.From(r)).ToList(),
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

    public static Movie To(NewMovieDto dto)
    {
        return new Movie(dto.Title, dto.Description, dto.ReleaseDate)
        {
            Reviews = new()
        };
    }
}