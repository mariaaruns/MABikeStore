using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.Contracts.IService
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(byte[] fileBytes,string fileName, string folderPath);
        Task<bool> DeleteFileAsync(string filePath);
       // Task<IFormFile> ConvertBase64StringToFile(string base64String);
    }
}
