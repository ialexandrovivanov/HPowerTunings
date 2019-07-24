using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Cloudinary
{
    public interface ICloudinaryService
    {
        Task<string> UploadPictureAsync(IFormFile pictureFile, string fileName);
    }
}