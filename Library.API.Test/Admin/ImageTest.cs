using Library.API.Controllers;
using Library.API.Models;
using RAIT.Core;

namespace Library.API.Test.Admin;

public class ImageTest : AuthTest
{
    [Test]
    public async Task UploadImage()
    {
        await LoginAdmin();
        FileUploadResult? fileUploadResult;
        var fileName = "ImageExample.png";
        using (var file = new RaitFormFile(fileName, "image/png"))
        {
           fileUploadResult = await Client.Call<ImageController, FileUploadResult>(n => n.UploadImage(file));
        }
      
        var serverFileBytes = await Client.GetByteArrayAsync(fileUploadResult!.Url);
        var originalFileBytes = await File.ReadAllBytesAsync(fileName);
        Assert.That(serverFileBytes.Length, Is.EqualTo(originalFileBytes.Length));
    }
}