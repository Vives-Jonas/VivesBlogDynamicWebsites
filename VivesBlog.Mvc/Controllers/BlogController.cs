using Microsoft.AspNetCore.Mvc;
using VivesBlog.Dto;
using VivesBlog.Model;
using VivesBlog.Services;

namespace VivesBlog.Mvc.Controllers
{
    public class BlogController(BlogService blogService, PersonService personService, IHttpClientFactory httpClientFactory) : Controller
    {

        [HttpGet]
        [Route("Vives-Blog")]
        public async Task<IActionResult> Index()
        {
            ViewData["IsDetail"] = false;
            var httpClient = httpClientFactory.CreateClient("VivesBlogApi");
            var response = await httpClient.GetAsync("Blog");
            response.EnsureSuccessStatusCode();

            var allBlogPosts = await response.Content.ReadFromJsonAsync<IList<ArticleDto>>();
            
            return View(allBlogPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            ViewData["IsDetail"] = true;
            var httpClient = httpClientFactory.CreateClient("VivesBlogApi");
            var response = await httpClient.GetAsync($"Blog/{id}");
            response.EnsureSuccessStatusCode();

            var blogPost = await response.Content.ReadFromJsonAsync<ArticleDto>();

            if (blogPost == null)
            {
                return NotFound($"Blog post with ID {id} not found.");
            }
            return View(blogPost);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return await CreateView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPost blogPost)
        {
            if (!ModelState.IsValid)
            {
                return await CreateView("Create", blogPost);
            }
            await blogService.Create(blogPost);
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var blogPost = await blogService.Get(id);
        //    if (blogPost is null)
        //    {
        //        return RedirectToAction("Index");
        //    }
            
        //    return await CreateView("Edit", blogPost);

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] BlogPost blogPost)
        {
            if (!ModelState.IsValid)
            {
                return await CreateView("Edit", blogPost);
            }

            await blogService.Update(id, blogPost);

            return RedirectToAction("Index");
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[controller]/Delete/{id:int?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await blogService.Delete(id);

            return RedirectToAction("Index");
        }


        private async Task<IActionResult> CreateView(string viewName, BlogPost? blogPost = null)
        {
            ViewBag.Authors = await personService.Find();
            if (blogPost is null)
            {
                return View(viewName);
            }
            return View(viewName, blogPost);
            
        }
    }
}
