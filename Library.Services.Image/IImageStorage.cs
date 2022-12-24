namespace Library.Services.Image;

public interface IImageStorage
{
    FileStream Get(string image);
    Task<string> Store(Stream bytes, string imageFileName, string imageContentType);
}