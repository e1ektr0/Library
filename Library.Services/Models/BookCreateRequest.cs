using Library.Data.Models;
using Mapster;

namespace Library.Services.Models;

public class BookCreateRequest
{
    public string Name { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string Author { get; set; } = null!;
    public long PagesCount { get; set; }
    public DateTime PublishedDate { get; set; }

    public Book ToDb()
    {
        var adapt = this.Adapt<Book>();
        return adapt;
    }
}