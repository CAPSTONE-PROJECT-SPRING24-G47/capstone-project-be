using capstone_project_be.Application.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly GCSConfigOptions _options;
        private readonly ILogger<BlogPhotoRepository> _logger;
        private readonly GoogleCredential _googleCredential;

        public StorageRepository(IOptions<GCSConfigOptions> options, ILogger<BlogPhotoRepository> logger)
        {
            _options = options.Value;
            _logger = logger;

            try
            {
                _googleCredential = GoogleCredential.FromFile(_options.GCPStorageAuthFile);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
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
                _logger.LogInformation($"File {fileNameToDelete} deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while deleting file {fileNameToDelete}: {ex.Message}");
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
                _logger.LogInformation($"Signed url obtained for file {fileNameToRead}");
                return signedUrl.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while obtaining signed url for file {fileNameToRead}: {ex.Message}");
                throw;
            }
        }

        public async Task<string> UpLoadFileAsync(IFormFile fileToUpload, string fileNameToSave)
        {
            try
            {
                _logger.LogInformation($"Uploading: file {fileNameToSave} to storage {_options.GoogleCloudStorageBucketName}");
                using (var memoryStream = new MemoryStream())
                {
                    await fileToUpload.CopyToAsync(memoryStream);
                    // Create Storage Client from Google Credential
                    using (var storageClient = StorageClient.Create(_googleCredential))
                    {
                        // upload file stream
                        var uploadedFile = await storageClient.UploadObjectAsync(_options.GoogleCloudStorageBucketName, fileNameToSave, fileToUpload.ContentType, memoryStream);
                        _logger.LogInformation($"Uploaded: file {fileNameToSave} to storage {_options.GoogleCloudStorageBucketName}");
                        return uploadedFile.MediaLink;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while uploading file {fileNameToSave}: {ex.Message}");
                throw;
            }
        }

        public class GCSConfigOptions
        {
            //Download key in GGCloud/IAM & Admin/Service Account
            public string? GCPStorageAuthFile { get; set; } = "C:\\Users\\Dell\\Downloads\\capstone-project-417405-b2b33810ff84.json";
            public string? GoogleCloudStorageBucketName { get; set; } = "capstone-project-storage";
        }
    }
}
