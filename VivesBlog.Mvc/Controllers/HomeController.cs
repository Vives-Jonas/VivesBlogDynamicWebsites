using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VivesBlog.Mvc.Models;
using VivesBlog.Services;

namespace VivesBlog.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogService _blogService;
        public HomeController(ILogger<HomeController> logger, BlogService blogService)
        {
            _logger = logger;
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            var blogPosts = _blogService.Find();
            return View(blogPosts);
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
