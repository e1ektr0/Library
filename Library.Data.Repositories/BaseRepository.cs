namespace Library.Data.Repositories;

public abstract class BaseRepository
{
    protected LibraryDbContext Context { get; }

    protected BaseRepository(LibraryDbContext dbContext)
    {
        Context = dbContext;
    }

    public async Task SaveAsync()
    {
        await Context.SaveChangesAsync();
    }
}