using Application.Interfaces;
using Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    class FileServiceRepository : IFileService
    {
        private readonly string _basePath;

        public FileServiceRepository(IOptions<FileStorageSettings> settings)
        {
            _basePath = settings.Value.BasePath;
        }

        public async Task<FileUploadResult> SaveFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                throw new Exception("File is empty");

            string folderPath = Path.Combine(_basePath, folderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string uniqueFileName = Guid.NewGuid().ToString()
                                    + Path.GetExtension(file.FileName);

            string fullPath = Path.Combine(folderPath, uniqueFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new FileUploadResult
            {
                FileName = uniqueFileName,
                RelativePath = Path.Combine(folderName, uniqueFileName),
                ContentType = file.ContentType,
                FileSize = file.Length
            };
        }

        public async Task<(byte[] FileBytes, string ContentType, string FileName)>
            DownloadFileAsync(string relativePath)
        {
            string fullPath = Path.Combine(_basePath, relativePath);

            if (!System.IO.File.Exists(fullPath))
                throw new FileNotFoundException("File not found");

            var bytes = await System.IO.File.ReadAllBytesAsync(fullPath);

            string contentType = "application/octet-stream";

            return (bytes, contentType, Path.GetFileName(fullPath));
        }

        public async Task DeleteFileAsync(string relativePath)
        {
            string fullPath = Path.Combine(_basePath, relativePath);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            await Task.CompletedTask;
        }
    }

    public class FileStorageSettings
    {
        public string BasePath { get; set; }
    }
}
