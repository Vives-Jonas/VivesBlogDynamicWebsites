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
            var allBlogPosts = _blogService.Find();
            return View(allBlogPosts);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            ViewData["IsDetail"] = true;
            var blogPost = _blogService.Get(id);
            
            if (blogPost == null)
            {
                return NotFound($"Blog post with ID {id} not found.");
            }
            return View(blogPost);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return CreateView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogPost blogPost)
        {
            if (!ModelState.IsValid)
            {
                return CreateView("Create", blogPost);
            }
            _blogService.Create(blogPost);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var blogPost = _blogService.Get(id);
            if (blogPost is null)
            {
                return RedirectToAction("Index");
            }
            
            return CreateView("Edit", blogPost);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, [FromForm] BlogPost blogPost)
        {
            if (!ModelState.IsValid)
            {
                return CreateView("Edit", blogPost);
            }

            _blogService.Update(id, blogPost);

            return RedirectToAction("Index");
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[controller]/Delete/{id:int?}")]
        public IActionResult DeleteConfirmed(int id)
        {
            _blogService.Delete(id);

            return RedirectToAction("Index");
        }


        private IActionResult CreateView(string viewName, BlogPost? blogPost = null)
        {
            ViewBag.Authors = _personService.Find();
            if (blogPost is null)
            {
                return View(viewName);
            }
            return View(viewName, blogPost);
            
        }
    }
}
