using System.Collections.Generic;
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
        [HttpPost("UploadFileAsync")]
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

        [HttpPost("UploadMultipleFilesAsync")]
        public async Task<IActionResult> UploadMultipleFilesAsync(List<IFormFile> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {

                    if (file.Length > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);

                        var path = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")).Root + $@"\{fileName}";

                        using (var stream = System.IO.File.Create(path))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                }
                return Ok("Done Suceesfully");
            }
            return Ok("Failed to upload your files");
        }
    }
}