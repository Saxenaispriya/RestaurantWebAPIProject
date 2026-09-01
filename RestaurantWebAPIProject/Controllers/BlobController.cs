using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs;

namespace RestaurantWebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : Controller
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobController(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please select a file.");
            }

            var containerClient =
                _blobServiceClient.GetBlobContainerClient("restaurant-files");

            var blobClient =
                containerClient.GetBlobClient(file.FileName);

            using var stream = file.OpenReadStream();

            await blobClient.UploadAsync(
                stream,
                overwrite: true);

            return Ok(new
            {
                Message = "File uploaded successfully",
                FileName = file.FileName,
                BlobUrl = blobClient.Uri.ToString()
            });
        }


        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var containerClient =
                _blobServiceClient.GetBlobContainerClient("restaurant-files");

            var blobClient =
                containerClient.GetBlobClient(fileName);

            if (!await blobClient.ExistsAsync())
            {
                return NotFound("File not found.");
            }

            var download = await blobClient.DownloadStreamingAsync();

            return File(
                download.Value.Content,
                download.Value.Details.ContentType ?? "application/octet-stream",
                fileName);
        }
    }
}
