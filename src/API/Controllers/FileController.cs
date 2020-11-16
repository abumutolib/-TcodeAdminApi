using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;

namespace API.Controllers
{
    [Authorize]
    public class FileController : ApiController
    {
        private readonly IConfiguration _config;

        public FileController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IFormFile file)
        {
            //var files = HttpContext.Request.Form.Files;
            var fileType = Path.GetExtension(file.FileName);
            //var ext = file.;
            var filePath = _config.GetValue<string>("RootFolderFiles");
            var docName = Path.GetFileName(file.FileName);

            var savePath = Path.Combine("RootPathFiles", $"{docName}-{Guid.NewGuid()}{fileType}");

            if (file != null && file.Length > 0)
            {
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            else
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        public void Delete(string path)
        {

        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
