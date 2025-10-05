using Microsoft.AspNetCore.Mvc;
using VivesBlog.Dto.Requests;
using VivesBlog.Sdk;

namespace VivesBlog.Mvc.Controllers
{
    public class PeopleController(PersonSdkService personSdkService) : Controller
    {
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            var result = await personSdkService.Find();
            return View(result);
        }

        public async Task<IActionResult> Detail(int id)
        {
            ViewData["IsDetail"] = true;

            var result = await personSdkService.Get(id);

            if (result == null)
            {
                return NotFound($"Person with ID {id} not found.");
            }
            return View(result);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await personSdkService.Create(request);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var result = await personSdkService.Get(id);
            if (result is null)
            {
                return RedirectToAction("Index");
            }

            var request = new PersonRequest
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
            };

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] PersonRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await personSdkService.Update(id, request);

            return RedirectToAction("Index");
        }

        

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            
            await personSdkService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
