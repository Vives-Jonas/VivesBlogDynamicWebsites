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
            var blogPosts = _database.BlogPosts;
            return View(blogPosts);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var blogPost = _database.BlogPosts.FirstOrDefault(b => b.ID == id);
            return View(blogPost);
        }
    }
}
