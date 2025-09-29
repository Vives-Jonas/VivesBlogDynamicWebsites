
using Microsoft.AspNetCore.Mvc;
using VivesBlog.Model;
using VivesBlog.Services;

namespace VivesBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController(BlogService blogService) : ControllerBase
    {

        //FIND
        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var articles = await blogService.Find();
            return Ok(articles);
        }

        //GET
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute]  int id)
        {
            var article = await blogService.Get(id);
            return Ok(article);
        }

        //CREATE
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] BlogPost blogPost)
        {
            var newArticle = await blogService.Create(blogPost);
            return Ok(newArticle);
        }

        //UPDATE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BlogPost blogPost)
        {
            var updatedArticle = await blogService.Update(id, blogPost);
            return Ok(updatedArticle);
        }

        //DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await blogService.Delete(id);
            return Ok();
        }
    }
}
