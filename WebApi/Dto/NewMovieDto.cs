namespace WebApi.Dto;

public class NewMovieDto
{
    public NewMovieDto(string title, string description, DateTime releaseDate)
    {
        Title = title;
        Description = description;
        ReleaseDate = releaseDate;
    }

    public string Title { get; init; }
    public string Description { get; init; }
    public DateTime ReleaseDate { get; init; } 
}