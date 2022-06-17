using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthorController : ControllerBase
    {
        public static List<Author> Authors=new List<Author>() { 
                new Author() { AuthorId = 10,AuthorName = "ola",AuthorLocation = "sana'a" },
                new Author() { AuthorId = 20,AuthorName = "Alia",AuthorLocation = "sana'a" },
            };

        [HttpGet]
        public List<Author> GetAllAuthor()
        {

            return Authors;
        }

        [HttpGet("{Id}")]
        public IActionResult GetAuthorByID(int Id)
        {
            var CurAuthor = Authors.Where(x => x.AuthorId == Id).FirstOrDefault();

            if (CurAuthor == null)
                return NotFound("Invalid Author");
            return Ok(CurAuthor);
        }

        [HttpPost]
        public IActionResult AddAuthor(Author NewAuthor)
        {
            if(string.IsNullOrWhiteSpace(NewAuthor.AuthorName))
            {
                return BadRequest("Invalid Empty Author Name");
            }

            var CurAuthor = Authors.Where(x => x.AuthorId == NewAuthor.AuthorId).SingleOrDefault();

            if (CurAuthor != null)
                return Conflict("Duplicate in Author Id");

            Authors.Add(NewAuthor);
            //return Ok();
            return CreatedAtAction("GetAuthorByID",new { Id=NewAuthor.AuthorId },NewAuthor);
        }

        [HttpPut]
        public IActionResult UpdateAuhor(Author updateauthor)
        {
            if(string.IsNullOrWhiteSpace(updateauthor.AuthorName))
            {
                return BadRequest("Invalid Empty Author Name");
            }

            var CurAuthor = Authors.Where(x => x.AuthorId == updateauthor.AuthorId).SingleOrDefault();

            if(CurAuthor==null)
            {
                return NotFound("Invalid Author");
            }

            CurAuthor.AuthorName = updateauthor.AuthorName;
            CurAuthor.AuthorLocation = updateauthor.AuthorLocation;

            return NoContent();
        }

        [HttpDelete("{Id}")]

        public IActionResult DeleteAuthor(int Id)
        {
            var CurAuthor = Authors.Where(x => x.AuthorId == Id).FirstOrDefault();

            if (CurAuthor == null)
                return NotFound("Invalid Author");
            Authors.Remove(CurAuthor);
            return Ok(CurAuthor);
        }

    }
}