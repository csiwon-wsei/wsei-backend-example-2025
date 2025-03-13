namespace WebApi.Dto;

public class MovieDto(string title, string description, DateTime releaseDate)
{
    public Guid Id { get; set; }
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
    public DateTime ReleaseDate { get; set; } = releaseDate;
    public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
}