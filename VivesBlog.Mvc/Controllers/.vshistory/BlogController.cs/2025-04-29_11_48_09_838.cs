using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VivesBlog.Repository;
using VivesBlog.Services;

namespace VivesBlog.Mvc.Controllers
{
    public class BlogController : Controller
    {

        
        private readonly BlogService _blogService;
        public BlogController(BlogService blogService)
        {
            
            _blogService = blogService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["IsDetail"] = false;
            var allBlogPosts = _blogService.GetAll();
            var count = allBlogPosts.Count;
            var withAuthorCount = allBlogPosts.Count(b => b.Author != null);

            //var blogPosts = _dbContext.BlogPosts
            //    .Include(b => b.Author)
            //    .ToList();
            return View(allBlogPosts);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            ViewData["IsDetail"] = true;
            var blogPost = _blogService.GetById(id);
            return View(blogPost);
        }
    }
}
