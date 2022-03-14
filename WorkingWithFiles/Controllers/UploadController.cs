using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace WorkingWithFiles.Controllers
{
    [Route("upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost("UploadFileAsyc")]
        public async Task<IActionResult> UploadAFileAsync(IFormFile file)
        {
            if (file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);

                var path = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")).Root + $@"\{fileName}";

                using (var stream = System.IO.File.Create(path))
                {
                    await file.CopyToAsync(stream);
                }
                return Ok("sucessfully done");
            }
            return Ok("No file detected to upload");
        }

    }
}