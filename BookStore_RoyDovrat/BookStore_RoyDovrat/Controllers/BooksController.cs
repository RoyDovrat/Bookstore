using BookStore_RoyDovrat.Models;
using BookStore_RoyDovrat.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_RoyDovrat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly XmlHandler _xmlHandler;

        public BooksController(XmlHandler xmlHandler)
        {
            _xmlHandler = xmlHandler;
        }


        [HttpGet("get_book")]
        public IActionResult GetBook(ulong isbn)
        {
            var book = _xmlHandler.GetBook(isbn);
            if (book != null)
            {
                return Ok(book);
            }
            return NotFound();

        }

        [HttpGet("get_all_books")]
        public IActionResult GetAllBooks()
        {
            var bookstore = _xmlHandler.LoadBooks();
            if (bookstore != null)
            {
                return Ok(bookstore);
            }
            return NotFound();
        }

        [HttpPost("add_book")]
        public IActionResult AddBook([FromBody] Book book)
        {
            if (_xmlHandler.AddBook(book))
            {
                return Ok(book);
            }
            return BadRequest("New book isbn is not valid");

        }

        [HttpDelete("remove_book")]
        public IActionResult RemoveBook(ulong isbnToDelete)
        {
            if (_xmlHandler.RemoveBook(isbnToDelete))
            {
                return Ok(isbnToDelete);
            }
            return BadRequest("Book isbn is not valid");


        }

        [HttpPut("update_book")]
        public IActionResult UpdateBook([FromBody] Book newBook)
        {
            if (_xmlHandler.UpdateBook(newBook))
            {
                return Ok(newBook);
            }
            return BadRequest("Book isbn does not exist");

        }

        [HttpGet("html_report")]
        public IActionResult CreateHtmlReports()
        {
            if (_xmlHandler.CreateHtmlReports())
            {
                return Ok();
            }
            return BadRequest("Failed to export html file");

        }

    }
}
