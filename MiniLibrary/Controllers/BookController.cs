using Microsoft.AspNetCore.Mvc;
using MiniLibrary.Entity;
using MiniLibrary.Models;
using MiniLibrary.Service.Interfaces;

namespace MiniLibrary.Controllers
{
    public class BookController : Controller
    {
        private IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            List<Book> booksList = await _bookService.GetAllAsync();

            return View(booksList);
        }

        public async Task<IActionResult> Add()
        {
            return View("Cadaster");
        }

        [HttpPost]
        public async Task<IActionResult> Add(Book book)
        {
            ResponseViewModel response = new();

            try
            {
                book.Id = string.Empty;
                Book addedBook = await _bookService.AddAsync(book);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }

            return Json(response);
        }

        public async Task<IActionResult> Edit(string id)
        {
            Book book = await _bookService.GetAsync(id);

            return View("Cadaster", book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            ResponseViewModel response = new();

            try
            {
                Book addedBook = await _bookService.EditAsync(book);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }

            return Json(response);
        }
    }
}
