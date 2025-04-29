using Microsoft.AspNetCore.Mvc;
using VivesBlog.Model;
using VivesBlog.Services;

namespace VivesBlog.Mvc.Controllers
{
    public class PeopleController : Controller
    {
        
        private readonly PersonService _personService;
        public PeopleController( PersonService personService)
        {

            
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            
            var people = _personService.Find();
            return View(people);
        }

       

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(BlogPost blogPost)
        {
            _blogService.Create(blogPost);
            return RedirectToAction("Index");
        }
    }
}
