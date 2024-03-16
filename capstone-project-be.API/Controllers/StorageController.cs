using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.IO;

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
            var stream = new MemoryStream();
            var obj = await client.
                DownloadObjectAsync("capstone-project-storage", filename, stream);
            stream.Position = 0;

            return File(stream, obj.ContentType, obj.Name);
        }

        [HttpPost]
        public async Task<IActionResult> AddFile([FromBody] FileUpload fileUpload)
        {
            var client = StorageClient.Create();
            var obj = await client.
                UploadObjectAsync("capstone-project-storage", fileUpload.Name, fileUpload.Type,
                new MemoryStream(fileUpload.File));

            return Ok(); 
        }

        public class FileUpload
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public byte[] File { get; set; }
        }
    }
}
