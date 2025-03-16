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
            var allBlogPosts = _dbContext.BlogPosts.ToList();
            var count = allBlogPosts.Count;
            var withAuthorCount = allBlogPosts.Count(b => b.Author != null);

            var blogPosts = _dbContext.BlogPosts
                .Include(b => b.Author)
                .ToList();
            return View(blogPosts);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var blogPost = _dbContext.BlogPosts
                .Include(b => b.Author)
                .ToList()
                .FirstOrDefault(b => b.Id == id);
            return View(blogPost);
        }
    }
}
