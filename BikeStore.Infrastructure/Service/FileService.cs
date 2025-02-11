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
        public void DeleteFile(string filePath)
        {
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folderPath)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(folderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return uniqueFileName;
        }
    }
}
