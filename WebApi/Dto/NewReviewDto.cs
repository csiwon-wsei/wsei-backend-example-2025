namespace WebApi.Dto;

public class NewReviewDto
{
    public Guid UserId { get; set; } 
    public string Title { get; set; } 
    public string Content { get; set; } 
    public uint Rate { get; set; }
}