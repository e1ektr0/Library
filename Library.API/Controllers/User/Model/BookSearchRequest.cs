namespace Library.API.Controllers.User.Model;

public class BookSearchRequest
{
    public string? Query { get; set; }
    public List<long>? Categories { get; set; } 
}