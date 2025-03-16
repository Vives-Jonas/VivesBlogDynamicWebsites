using Microsoft.AspNetCore.Mvc;

namespace VivesBlog.Mvc.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
