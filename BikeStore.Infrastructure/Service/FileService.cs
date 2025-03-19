using BikeStore.Domain.Contracts.IService;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Service
{
    public class FileService : IFileService
    {
       /* public Task<IFormFile> ConvertBase64StringToFile(string base64String)
        {
            var mimeType = base64String.Substring(5, base64String.IndexOf(";") - 5);

            // Remove the prefix (data:image/png;base64,) and decode Base64
            var base64Data = base64String.Substring(base64String.IndexOf(",") + 1);
            var fileBytes = Convert.FromBase64String(base64Data);

            var fileExtension = mimeType.Split('/').Last(); // Extract file extension (e.g., png, jpeg, jpg, gif)

            var formFile = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "logo", $"brand-logo.{fileExtension}")
            {
                Headers = new HeaderDictionary(),
                ContentType = mimeType  // Use the extracted MIME type
            };

        }*/

        public  async Task<bool> DeleteFileAsync(string filePath)
        {
            if (File.Exists(filePath)) 
            {
                File.Delete(filePath);
                await Task.CompletedTask;
                return true;
            }
            return false ;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folderPath)
        {
            if (file.Length > 0)
            {
                var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine(folderPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return uniqueFileName;
            }

            return string.Empty;
        }
    }
}
