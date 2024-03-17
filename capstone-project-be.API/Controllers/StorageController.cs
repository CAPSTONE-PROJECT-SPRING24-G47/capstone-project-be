using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Identity.Client.Extensions.Msal;
using System.IO;
using System.Security.AccessControl;

namespace capstone_project_be.API.Controllers
{
    [Route("api/storage-controller")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly ILogger<StorageController> _logger;

        public StorageController(ILogger<StorageController> logger) 
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetFile(string filename)
        {
            var client = StorageClient.Create();
            var imageUrl = client.GetObjectAsync("capstone-project-storage", filename);

            var stream = new MemoryStream();
            var obj = await client.
                DownloadObjectAsync("capstone-project-storage", filename, stream);
            stream.Position = 0;

            return File(stream, obj.ContentType, obj.Name);
        }

        public class FileUpload
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public byte[] File { get; set; }
        }
    }
}
