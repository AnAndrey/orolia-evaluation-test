using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Orolia.DataParcerInterfaces;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebService.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet("{id}")]
        public IEnumerable<SSDData> Get(string name)
        {
            //return new List<string>() {"dfg", "1111"};
            return DataParcer.ParceFile(Path.Combine(FileManageSettings.FilePath, "big.ssd"));
        }
    }
}
