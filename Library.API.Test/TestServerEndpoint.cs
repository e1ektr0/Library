using Library.Data;

namespace Library.API.Test;

public class TestServerEndpoint
{
    public IServiceProvider Services { get; set; } = null!;
    public HttpClient WebClient { get; set; } = null!;
    public LibraryDbContext Context { get; set; } = null!;
}