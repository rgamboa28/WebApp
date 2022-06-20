using Microsoft.AspNetCore.Mvc;

namespace ForDemoPurposeOnly.Views.Pages
{
    public class PagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Product()
        {
            return View();
        }
    }
}
