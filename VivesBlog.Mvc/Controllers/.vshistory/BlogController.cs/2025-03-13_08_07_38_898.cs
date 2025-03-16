using Microsoft.AspNetCore.Mvc;
using VivesBlog.Mvc.Core;

namespace VivesBlog.Mvc.Controllers
{
    public class BlogController : Controller
    {

        private readonly BlogPostDatabase _database;
        public BlogController(BlogPostDatabase database)
        {
            _database = database;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var blogposts = _database.BlogPosts;
            return View();
        }
    }
}
