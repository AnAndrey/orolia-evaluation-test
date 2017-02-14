using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Orolia.FileManagerInterfaces;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class FilesController : Controller
    {
        private FileManageSettings FileManageSettings { get; }
        private IFileManager FileManager { get; }

        public FilesController(IOptions<FileManageSettings> settings, IFileManager fileManager)
        {
            FileManageSettings = settings.Value;
            FileManager = fileManager;
        }

        // GET api/files
        [HttpGet]
        public IEnumerable<string> GetAllFiles()
        {
            return FileManager.GetFiles(FileManageSettings.FilePath);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostFile()
        {
            var files = Request.Form.Files;
            if (files == null || files.Count == 0)
                return new OkResult();

            var copiedFiles = new List<string>();
            try
            {
                foreach (var formFile in files)
                {
                    if (formFile.Length <= 0) continue;

                    var filePath = Path.GetTempFileName();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    var filename = FileManager.Move(filePath, Path.Combine( FileManageSettings.FilePath, formFile.FileName));
                    if(!String.IsNullOrEmpty(filename)) copiedFiles.Add(filename);
                }
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }

            var size = files.Sum(f => f.Length);

            return Ok(new { count = files.Count, size, copiedFiles});
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{name}")]
        public void DeleteFile(string name)
        {
            FileManager.Delete(Path.Combine( FileManageSettings.FilePath, name));
        }
    }
}
