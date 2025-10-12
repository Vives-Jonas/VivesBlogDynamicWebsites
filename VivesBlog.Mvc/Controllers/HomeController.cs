using Microsoft.AspNetCore.Mvc;
using VivesBlog.Sdk;

namespace VivesBlog.Mvc.Controllers
{
    public class HomeController(ILogger<HomeController> logger, BlogSdkService blogSdkService) : Controller
    {
        

        public async Task<IActionResult> Index()
        {
            var result = await blogSdkService.GetRandom();
            return View(result);
        }

    }
}
