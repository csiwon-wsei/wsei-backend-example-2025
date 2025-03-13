using System.Text.Json.Serialization;
using ApplicationCore.Application.Commons;
using ApplicationCore.Domain.ValueObject;

namespace ApplicationCore.Domain.Models;

public class Review(Guid movieId, Guid userId, string title, string content, ReviewRate rate): BaseIdentity
{
    public Guid MovieIdGuid { get; set; } = movieId;
    
    public Guid UserIdGuid { get; set; } = userId;
    public string Title { get; set; } = title;
    public string Content { get; set; } = content;
    public ReviewRate Rate => rate;
    public DateTime CreteDateTime { get; set; }
    public Movie? Movie { get; set; }
    public User? User { get; set; }
}