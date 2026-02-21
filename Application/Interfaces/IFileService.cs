using Domain.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFileService
    {
        Task<FileUploadResult> SaveFileAsync(IFormFile file, string folderName);
        Task<(byte[] FileBytes, string ContentType, string FileName)> DownloadFileAsync(string relativePath);
        Task DeleteFileAsync(string relativePath);
    }
}
