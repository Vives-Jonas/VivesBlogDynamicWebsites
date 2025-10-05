using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VivesBlog.Mvc.Models;
using VivesBlog.Sdk;

namespace VivesBlog.Mvc.Controllers
{
    public class HomeController(ILogger<HomeController> logger, BlogSdkService blogSdkService) : Controller
    {
        

        public async Task<IActionResult> Index()
        {
            var result = await blogSdkService.Find();
            return View(result);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
