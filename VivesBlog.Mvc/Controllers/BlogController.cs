using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VivesBlog.Model;
using VivesBlog.Repository;
using VivesBlog.Services;

namespace VivesBlog.Mvc.Controllers
{
    public class BlogController : Controller
    {

        
        private readonly BlogService _blogService;
        private readonly PersonService _personService;
        public BlogController(BlogService blogService, PersonService personService)
        {
            
            _blogService = blogService;
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["IsDetail"] = false;
            var allBlogPosts = _blogService.GetAll();
            return View(allBlogPosts);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            ViewData["IsDetail"] = true;
            var blogPost = _blogService.GetById(id);
            return View(blogPost);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var authors = _personService.Find();
            ViewBag.Authors = authors;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogPost blogPost)
        {
            _blogService.Create(blogPost);
            return RedirectToAction("Index");
        }
    }
}
