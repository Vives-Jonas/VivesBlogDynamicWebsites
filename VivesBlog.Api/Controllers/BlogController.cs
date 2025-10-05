
using Microsoft.AspNetCore.Mvc;
using VivesBlog.Dto.Requests;
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
            var result = await blogService.Find();
            return Ok(result);
        }

        //GET
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute]  int id)
        {
            var result = await blogService.Get(id);
            return Ok(result);
        }

        //CREATE
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] ArticleRequest request)
        {
            var result = await blogService.Create(request);
            return Ok(result);
        }

        //UPDATE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ArticleRequest request)
        {
            var result = await blogService.Update(id, request);
            return Ok(result);
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
