using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [Route("api/Author/{AuthorId}/Book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public static List<Book> Booklist = new List<Book>() {
                new Book() { BookId = 1,BookName = "API",ParperCount =300,PublishYear=2019,AuthorId= 10  },
                new Book() { BookId = 2,BookName = "C#",ParperCount =200,PublishYear=2000,AuthorId= 10 },
                new Book() { BookId = 3,BookName = "ASPNET",ParperCount =500,PublishYear=2019,AuthorId= 20 },
            };

        [HttpGet]
        public IActionResult GetAllBook(int AuthorId)
        {

            return Ok(Booklist.Where(x => x.AuthorId == AuthorId).ToList());
        }
        [HttpGet("{BookId}")]
        public IActionResult GetAllBookForAuthor(int AuthorId, int BookId)
        {
            var CurBook = Booklist.Where(x => x.AuthorId == AuthorId && x.BookId == BookId).SingleOrDefault();
            if (CurBook == null)
                return NotFound("Book Not Found");

            return Ok(CurBook);
        }
        [HttpPost]
        public IActionResult AddBook(int AuthorId ,Book NewBook)
        {
            if (!AuthorController.Authors.Any(x => x.AuthorId == AuthorId))
                return NotFound("Author Not Found");
            if (AuthorId != NewBook.AuthorId)
                return BadRequest("Invalid Author Id");
            if (Booklist.Any(x => x.AuthorId == AuthorId && x.BookId == NewBook.BookId))
                return Conflict("Book is Already Exist");

            Booklist.Add(NewBook);
            return CreatedAtAction(nameof(GetAllBookForAuthor), new { AuthorId = AuthorId, BookId = NewBook.BookId }, NewBook);
        }

        [HttpDelete("{BookId}")]
        public IActionResult DeleteBook(int AuthorId, int BookId)
        {
            if (!AuthorController.Authors.Any(x => x.AuthorId == AuthorId))
                return NotFound("Author Not Found");

            var CurBook = Booklist.Where(x => x.AuthorId == AuthorId && x.BookId == BookId).SingleOrDefault();
               if(CurBook==null)
                  return NotFound("Book Not Found");

            Booklist.Remove(CurBook);
            return NoContent();
        }
    }

}