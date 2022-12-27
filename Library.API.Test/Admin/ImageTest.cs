using Library.API.Controllers;
using Library.API.Models;
using RAIT.Core;

namespace Library.API.Test.Admin;

public sealed  class ImageTest : BaseApiTest
{
    [Test]
    public async Task UploadImage()
    {
        await Base<AuthTest>().LoginAdmin();
        FileUploadResult? fileUploadResult;
        var fileName = "ImageExample.png";
        using (var file = new RaitFormFile(fileName, "image/png"))
        {
           fileUploadResult = await Rait<ImageController>().Call(n => n.UploadImage(file));
        }
      
        var serverFileBytes = await WebClient.GetByteArrayAsync(fileUploadResult!.Url);
        var originalFileBytes = await File.ReadAllBytesAsync(fileName);
        Assert.That(serverFileBytes.Length, Is.EqualTo(originalFileBytes.Length));
    }
}