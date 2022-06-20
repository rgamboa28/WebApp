using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForDemoPurposeOnly.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;

namespace ForDemoPurposeOnly.Controllers
{
    public class WatchDetailsController : Controller
    {
        private string baseApiUrl;
        public WatchDetailsController(IConfiguration configuration)
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
                response = client.GetStringAsync("api/WatchDetails/").Result;
                var parameter = JsonConvert.DeserializeObject<List<WatchDetails>>(response);
                return View(parameter);
            }
        }

        // GET: WatchDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WatchDetails watch = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);

                var response = client.GetStringAsync("api/WatchDetails/" + id.ToString()).Result;
                watch = JsonConvert.DeserializeObject<WatchDetails>(response);

            }
            if (watch == null)
            {
                return NotFound();
            }

            return View(watch);
        }

        // GET: WatchDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WatchDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WatchId,WatchName,SerialNo,Price,WatchWeight,WatchHeight,WatchDiameter,WatchThickness,ShortDesc,Caliber,Movement,Chronograph,Jewel,CaseMaterial,StrapMaterial,FullDesc")] WatchDetails watchDetails)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    var response = client.PostAsJsonAsync("api/WatchDetails/", watchDetails).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }

            }
            return View(watchDetails);
        }

        // GET: WatchDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }   

            WatchDetails watch = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);

                var response = client.GetStringAsync("api/WatchDetails/" + id.ToString()).Result;
                watch = JsonConvert.DeserializeObject<WatchDetails>(response);

            }

            if (watch == null)
            {
                return NotFound();
            }

            return View(watch);
        }

        // POST: WatchDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WatchId,WatchName,SerialNo,Price,WatchWeight,WatchHeight,WatchDiameter,WatchThickness,ShortDesc,Caliber,Movement,Chronograph,Jewel,CaseMaterial,StrapMaterial,FullDesc")] WatchDetails watchDetails)
        {

            if (id != watchDetails.WatchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseApiUrl);
                        var response = client.PutAsJsonAsync("api/WatchDetails/" + id.ToString(), watchDetails).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }

                    }
                }
                catch (Exception)
                {

                }

            }
            return View(watchDetails);
        }

        // GET: WatchDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WatchDetails watch = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);

                var response = client.GetAsync("api/WatchDetails/" + id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<WatchDetails>();
                    read.Wait();

                    watch = read.Result;
                }
            }
            if (watch == null)
            {
                return NotFound();
            }

            return View(watch);
        }

        // POST: WatchDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);

                var delete = client.DeleteAsync("api/WatchDetails/" + id.ToString());
                delete.Wait();

                var result = delete.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

            }

            return RedirectToAction(nameof(Index));
        }

    }
}
