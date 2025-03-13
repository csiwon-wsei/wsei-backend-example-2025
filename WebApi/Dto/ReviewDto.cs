using ApplicationCore.Application.Commons;
using ApplicationCore.Domain.Models;
using ApplicationCore.Domain.ValueObject;

namespace WebApi.Dto;

public class ReviewDto(Guid userId, string title, string content, ReviewRate rate): BaseIdentity
{
    public Guid UserId { get; set; } = userId;
    public string Title { get; set; } = title;
    public string Content { get; set; } = content;
    public uint Rate => rate.Value;
}