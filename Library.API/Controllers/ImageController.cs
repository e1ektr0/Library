using Library.Services;
using Library.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("image")]
[ResponseCache(Duration = 25920000)]
public class ImageController : ControllerBase
{
    private readonly ImageService _imageService;

    public ImageController(ImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet]
    [Route("{image}")]
    public async Task<FileStreamResult> GetImage(string image, [FromQuery] string contentType)
    {
        var stream = _imageService.Get(image);
        await Task.CompletedTask;
        return new FileStreamResult(stream, contentType);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = CustomRoles.Admin)]
    public async Task<string> UploadImage(IFormFile image)
    {
        return await _imageService.Store(image.OpenReadStream(), image.FileName, image.ContentType);
    }
}