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
