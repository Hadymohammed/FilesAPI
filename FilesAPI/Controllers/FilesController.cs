using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly FilesService _filesService;
        public FilesController(FilesService filesService)
        {
            _filesService = filesService;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var newFileName = await _filesService.UploadFile(file);
            return Ok(newFileName);
        }
        [HttpGet]
        public IActionResult GetAllFiles()
        {
            var files = _filesService.GetAllFiles();
            return Ok(files);
        }
        [HttpGet("{id}")]
        public IActionResult GetFile(int id)
        {
            var file = _filesService.GetFileById(id);
            return Ok(file);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            await _filesService.DeleteFile(id);
            return Ok();
        }
        //download file
        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var file = _filesService.GetFileById(id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(file.FilePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, file.Type, file.Title);
            // var fileBytes = _filesService.DownloadFile(id);

        }
    }
}