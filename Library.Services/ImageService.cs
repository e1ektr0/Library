using Library.Services.Image;

namespace Library.Services;

public class ImageService
{
    private readonly IImageStorage _imageStorage;

    public ImageService(IImageStorage imageStorage)
    {
        _imageStorage = imageStorage;
    }

    public async Task<string> Store(Stream input, string imageFileName, string imageContentType)
    {
        return await _imageStorage.Store(input, imageFileName, imageContentType);
    }

    public FileStream Get(string image)
    {
        return _imageStorage.Get(image);
    }
}