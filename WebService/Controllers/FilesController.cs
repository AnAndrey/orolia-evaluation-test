using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
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

        //// GET api/files/5
        //[HttpGet("{id}")]
        //public string Get(string id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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

    public class FileManageSettings
    {
        public string FilePath { get; set; }
    }
}
