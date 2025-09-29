using Microsoft.AspNetCore.Mvc;
using VivesBlog.Model;
using VivesBlog.Services;

namespace VivesBlog.Mvc.Controllers
{
    public class PeopleController(PersonService personService) : Controller
    {
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            var people = await personService.Find();
            return View(people);
        }

       

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {
            await personService.Create(person);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var person = await personService.Get(id);
            if (person is null)
            {
                return RedirectToAction("Index");
            }
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] Person person)
        {
            await personService.Update(id, person);
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
        public async Task<IActionResult> Delete(int id)
        {
            
            await personService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
