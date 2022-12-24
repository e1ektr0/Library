namespace Library.API.Controllers.Admin.Models;

public class BookUpdateRequest
{
    public long BookId { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public string? Author { get; set; }
    public long? PagesCount { get; set; }
    public DateTime? PublishedDate { get; set; }
}