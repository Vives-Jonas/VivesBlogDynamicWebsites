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
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {
            _personService.Create(person);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute]int id)
        {
            var person = _personService.Get(id);
            if (person is null)
            {
                return RedirectToAction("Index");
            }
            return View(person);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, [FromForm] Person person)
        {
            _personService.Update(id, person);
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public IActionResult Delete([FromRoute] int id)
        //{
        //    var person = _personService.Get(id);
        //    if (person is null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View(person);
        //}

        [HttpPost]
        public IActionResult Delete(int id)
        {
            
            _personService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
