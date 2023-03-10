using NpgsqlTypes;

namespace Library.Data.Models;

public class Book : BaseEntity
{
    public string Name { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string Author { get; set; } = null!;
    public long PagesCount { get; set; }
    public DateTime PublishedDate { get; set; }
    public NpgsqlTsVector EngSearchVector { get; set; } = null!;
    public List<BookCategory>? Categories { get; set; }
}