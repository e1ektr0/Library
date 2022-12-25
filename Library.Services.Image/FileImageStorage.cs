namespace Library.Services.Image;

public class FileImageStorage : IImageStorage
{
    public FileStream Get(string image)
    {
        return File.Open(image, FileMode.Open);
    }

    public async Task<string> Store(Stream input, string imageFileName, string imageContentType)
    {
        var path = Guid.NewGuid().ToString();
        await using Stream file = File.Create(path);
        CopyStream(input, file);
        return "/image/" + path + "?contentType=" + imageContentType;
    }

    private static void CopyStream(Stream input, Stream output)
    {
        var buffer = new byte[8 * 1024];
        int len;
        while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
        {
            output.Write(buffer, 0, len);
        }
    }
}