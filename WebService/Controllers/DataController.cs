using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Orolia.DataParcerInterfaces;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class DataController : Controller
    {
        private FileManageSettings FileManageSettings { get; }
        private IDataParcer DataParcer { get; }

        public DataController(IOptions<FileManageSettings> settings, IDataParcer dataParcer)
        {
            FileManageSettings = settings.Value;
            DataParcer = dataParcer;
        }

        // GET api/values/5
        [HttpGet("{name}")]
        public IEnumerable<SSDData> Get(string name)
        {
            return DataParcer.ParceFile(Path.Combine(FileManageSettings.FilePath, name));
        }
    }
}
