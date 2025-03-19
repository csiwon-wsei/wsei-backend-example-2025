using ApplicationCore.Application.Commons;
using ApplicationCore.Domain.Models;
using ApplicationCore.Domain.ValueObject;

namespace WebApi.Dto;

public class ReviewDto()
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public uint Rate { get; set; }
}