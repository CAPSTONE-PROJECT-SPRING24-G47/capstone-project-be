using Microsoft.AspNetCore.Http;

namespace capstone_project_be.Application.Interfaces
{
    public interface IStorageRepository
    {
        Task<string> GetSignedUrlAsync(string fileNameToRead, int timeOutInMinutes = 30);
        Task<string> UpLoadFileAsync(IFormFile fileToUpload, string fileNameToSave);
        Task DeleteFileAsync(string fileNameToDelete);
        Task<string> GetFileAsBase64Async(string fileName);
    }
}
