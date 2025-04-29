using Microsoft.AspNetCore.Mvc;

namespace VivesBlog.Mvc.Controllers
{
    public class PeopleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
