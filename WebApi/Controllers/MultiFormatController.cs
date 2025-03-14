using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class MultiFormatController : ControllerBase
    {
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
        public IActionResult GetAll()
        {
            var books =
                    Enumerable.Range(1, 10).Select(i =>
                        new Book
                        {
                            Title = Random.Shared.GetItems(["C#", "Java", "Asp.net", "PHP"], 1)[0],
                            Author = Random.Shared.GetItems(["Ewa", "Adam", "Karol", "Iwona"], 1)[0],
                            Pages = Random.Shared.Next(20, 100),
                            Id = i
                        }
                    ).ToList();
            return Ok(books);
        }
    }
}

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Pages { get; set; }
}