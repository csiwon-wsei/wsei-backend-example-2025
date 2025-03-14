using ApplicationCore.Application.Commons;

namespace ApplicationCore.Domain.Models;

public class Movie(string title, string description, DateTime releaseDate): BaseIdentity
{
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
    public DateTime ReleaseDate { get; set; } = releaseDate;
    public List<Review> Reviews { get; set; } = new ();

    public decimal AverageRate
    {
        get
        {
            return Reviews.Average(r => (decimal) r.Rate);
        }
    }
}