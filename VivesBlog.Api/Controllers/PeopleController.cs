﻿using Microsoft.AspNetCore.Mvc;
using VivesBlog.Model;
using VivesBlog.Services;

namespace VivesBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController(PersonService personService) : ControllerBase
    {
        //FIND
        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var people = await personService.Find();
            return Ok(people);
        }


        //GET
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var people = await personService.Get(id);
            return Ok(people);
        }


        //CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Person person)
        {
            var newPerson = await personService.Create(person);
            return Ok(newPerson);
        }


        //UPDATE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Person person)
        {
            var updatedPerson = await personService.Update(id, person);
            return Ok(updatedPerson);
        }


        //DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await personService.Delete(id);
            return Ok();
        }
    }
}