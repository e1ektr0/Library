namespace Library.API.Controllers.User.Model;

public class BookDto
{
    public string Name { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string Author { get; set; } = null!;
    public long PagesCount { get; set; }
    public DateTime PublishedDate { get; set; }
}