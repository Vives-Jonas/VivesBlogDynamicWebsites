using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VivesBlog.Mvc.Core;

namespace VivesBlog.Mvc.Controllers
{
    public class BlogController : Controller
    {

        private readonly BlogPostDbContext _dbContext;
        public BlogController(BlogPostDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var blogPosts = _dbContext.BlogPosts.Include(b => b.Author).ToList();
            return View(blogPosts);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var blogPost = _dbContext.BlogPosts.FirstOrDefault(b => b.ID == id);
            return View(blogPost);
        }
    }
}
