using System;
using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NuGet.Protocol.Core.v3;
using WebApplication.DTO;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    /// <summary>
    /// A controller intercepts the incoming browser request and returns
    /// an HTML view (.cshtml file) or any other type of data.
    /// </summary>
    public class HomeController : Controller
    {
        static public IConfigurationRoot Configuration { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Settings(string angularModuleName = "app-main.settings")
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();


            var settingsDto = new SettingsDto()
            {
                FileServiceUrl = Configuration["FileServiceUrl"],
                DataServiceUrl = Configuration["DataServiceUrl"]
            };

            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var settingsJson = JsonConvert.SerializeObject(settingsDto, Formatting.Indented, serializerSettings);

            var settingsModel = new SettingsViewModel()
            {
                SettingsJson = settingsJson,
                AngularModuleName = angularModuleName
            };

            Response.ContentType = "text/javascript";

            return View(settingsModel);
        }
    }
}
