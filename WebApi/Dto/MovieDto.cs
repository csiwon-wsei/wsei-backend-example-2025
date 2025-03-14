namespace WebApi.Dto;

public class MovieDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
}