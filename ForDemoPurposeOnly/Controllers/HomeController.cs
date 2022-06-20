using ForDemoPurposeOnly.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ForDemoPurposeOnly.Controllers
{
    public class HomeController : Controller
    {
        private string baseApiUrl;

        public HomeController(IConfiguration configuration)
        {
            baseApiUrl = configuration.GetValue<string>("ApiUrl");
        }

        // GET: WatchDetails
        public async Task<IActionResult> Index()
        {
            string response = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                response = client.GetStringAsync("api/WatchDetails").Result;
                var parameter = JsonConvert.DeserializeObject<List<WatchDetails>>(response);
                return View(parameter);
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
