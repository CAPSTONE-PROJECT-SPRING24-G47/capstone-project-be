using capstone_project_be.Application.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly GCSConfigOptions _options;
        private readonly GoogleCredential _googleCredential;

        public StorageRepository(IOptions<GCSConfigOptions> options)
        {
            _options = options.Value;

            try
            {
                _googleCredential = GoogleCredential.FromFile(_options.GCPStorageAuthFile);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteFileAsync(string fileNameToDelete)
        {
            try
            {
                using (var storageClient = StorageClient.Create(_googleCredential))
                {
                    await storageClient.DeleteObjectAsync(_options.GoogleCloudStorageBucketName, fileNameToDelete);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> GetSignedUrlAsync(string fileNameToRead, int timeOutInMinutes = 30)
        {
            try
            {
                var sac = _googleCredential.UnderlyingCredential as ServiceAccountCredential;
                var urlSigner = UrlSigner.FromServiceAccountCredential(sac);
                // provides limited permission and time to make a request: time here is mentioned for 30 minutes.
                var signedUrl = await urlSigner.SignAsync(_options.GoogleCloudStorageBucketName, fileNameToRead, TimeSpan.FromMinutes(timeOutInMinutes));
                return signedUrl.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> UpLoadFileAsync(IFormFile fileToUpload, string fileNameToSave)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await fileToUpload.CopyToAsync(memoryStream);
                    // Create Storage Client from Google Credential
                    using (var storageClient = StorageClient.Create(_googleCredential))
                    {
                        // upload file stream
                        var uploadedFile = await storageClient.UploadObjectAsync(_options.GoogleCloudStorageBucketName, fileNameToSave, fileToUpload.ContentType, memoryStream);
                        return uploadedFile.MediaLink;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> GetFileAsBase64Async(string fileName)
        {
            try
            {
                using (var storageClient = StorageClient.Create(_googleCredential))
                {
                    var memoryStream = new MemoryStream();
                    await storageClient.DownloadObjectAsync(_options.GoogleCloudStorageBucketName, fileName, memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin); // Reset stream position to the beginning

                    var byteArray = memoryStream.ToArray();
                    var base64String = Convert.ToBase64String(byteArray);

                    return base64String;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IFormFile> GetIFormFileFromBase64Async(string base64String, string fileName)
        {
            try
            {
                // Chuyển đổi chuỗi base64 thành mảng byte
                byte[] byteArray = Convert.FromBase64String(base64String);

                // Tạo một MemoryStream từ mảng byte
                using (MemoryStream memoryStream = new MemoryStream(byteArray))
                {
                    // Tạo một đối tượng FormFile từ MemoryStream
                    IFormFile formFile = new FormFile(memoryStream, 0, memoryStream.Length, null, fileName);

                    // Trả về đối tượng IFormFile
                    return formFile;
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine($"Error while converting base64 to IFormFile: {ex.Message}");
                throw;
            }
        }

        public async Task<string> UploadFileFromBase64Async(string base64String, string fileNameToSave)
        {
            try
            {
                // Chuyển đổi chuỗi base64 thành mảng byte
                byte[] byteArray = Convert.FromBase64String(base64String);

                // Tạo một MemoryStream từ mảng byte
                using (MemoryStream memoryStream = new MemoryStream(byteArray))
                {
                    // Create Storage Client from Google Credential
                    using (var storageClient = StorageClient.Create(_googleCredential))
                    {
                        // Upload file stream
                        var uploadedFile = await storageClient.UploadObjectAsync(
                            _options.GoogleCloudStorageBucketName,
                            fileNameToSave,
                            "application/octet-stream", // Assume content type is application/octet-stream for images
                            memoryStream);

                        // Trả về đường dẫn tới file trên Google Drive
                        return uploadedFile.MediaLink;
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine($"Error while uploading file {fileNameToSave}: {ex.Message}");
                throw;
            }
        }

        public class GCSConfigOptions
        {
            //Download key in GGCloud/IAM & Admin/Service Account
            public string? GCPStorageAuthFile { get; set; } = "C:\\Users\\phamm\\Downloads\\capstone-project-417405-b2b33810ff84.json";
            public string? GoogleCloudStorageBucketName { get; set; } = "capstone-project-storage";
        }
    }
}
