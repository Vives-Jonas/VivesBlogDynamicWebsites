using Microsoft.AspNetCore.Mvc;
using VivesBlog.Dto.Requests;
using VivesBlog.Mvc.Extensions;
using VivesBlog.Sdk;

namespace VivesBlog.Mvc.Controllers
{
    public class BlogController(BlogSdkService blogSdkService, PersonSdkService personSdkService) : Controller
    {

        [HttpGet]
        [Route("Vives-Blog")]
        public async Task<IActionResult> Index([FromQuery] int? authorId)
        {
            ViewData["IsDetail"] = false;
            var result = await blogSdkService.Find(authorId);
            
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            ViewData["IsDetail"] = true;

            var result = await blogSdkService.Get(id);

            if (result == null)
            {
                return NotFound($"Blog post with ID {id} not found.");
            }
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return await CreateView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ArticleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return await CreateView("Create", request);
            }
            var result = await blogSdkService.Create(request);

            if (!result.IsSuccess)
            {
                ModelState.AddServiceMessages(result.Messages);

                return View(request);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await blogSdkService.Get(id);
            if (result is null)
            {
                return RedirectToAction("Index");
            }

            var request = new ArticleRequest()
            {
                Title = result.Title,
                Content = result.Content,
                AuthorId = result.AuthorId,
            };

            return await CreateView("Edit", request);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] ArticleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return await CreateView("Edit", request);
            }

            var result = await blogSdkService.Update(id, request);

            if (!result.IsSuccess)
            {
                ModelState.AddServiceMessages(result.Messages);
                return await CreateView("Edit", request);
            }

            return RedirectToAction("Index");
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await blogSdkService.Delete(id);
            if (!result.IsSuccess)
            {
                ModelState.AddServiceMessages(result.Messages);
                return View();
            }

            return RedirectToAction("Index");
        }


        private async Task<IActionResult> CreateView(string viewName, ArticleRequest? request = null)
        {
            ViewBag.Authors = await personSdkService.Find();
            if (request is null)
            {
                return View(viewName);
            }
            return View(viewName, request);
            
        }
    }
}
