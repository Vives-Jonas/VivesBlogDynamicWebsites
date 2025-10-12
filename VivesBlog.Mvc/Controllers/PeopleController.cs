using Microsoft.AspNetCore.Mvc;
using VivesBlog.Dto.Requests;
using VivesBlog.Mvc.Extensions;
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

            var result = await personSdkService.Create(request);
            if (!result.IsSuccess)
            {
                ModelState.AddServiceMessages(result.Messages);
                return View(request);
            }
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

            var result = await personSdkService.Update(id, request);

            if (!result.IsSuccess)
            {
                ModelState.AddServiceMessages(result.Messages);
                return View(request);
            }

            return RedirectToAction("Index");
        }

        

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            
            var result = await personSdkService.Delete(id);
            if (!result.IsSuccess)
            {
                ModelState.AddServiceMessages(result.Messages);
                return View();
            }

            return RedirectToAction("Index");
        }
    }
}
