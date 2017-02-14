using System;
using System.Collections.Generic;
using System.IO;
using System;
using WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NuGet.Protocol.Core.v3;

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
            // The view being returned is calculated based on the name of the
            // controller (Home) and the name of the action method (Index).
            // So in this case, the view returned is /Views/Home/Index.cshtml.

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
                WebApiBaseUrl = Configuration["WebApiBaseUrl"]
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

        public IActionResult About()
        {
            // Creates a model and passes it on to the view.
            Employee[] model =
            {
                new Employee { Name = "Alfred", Title = "Manager" },
                new Employee { Name = "Sarah", Title = "Accountant" }
            };

            return View(model);
        }

 
    }

    public class SettingsDto
    {
        public string WebApiBaseUrl { get; set; }
        public string WebApiVersion { get; set; }
    }

    public class SettingsViewModel
    {
        public string SettingsJson { get; set; }
        public string AngularModuleName { get; set; }
    }
}
