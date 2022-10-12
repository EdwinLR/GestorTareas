using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GestorTareas.Web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string nameFile, string folder);
        Task<string> UpdateImageAsync(IFormFile imageFile, string url);
    }
}
