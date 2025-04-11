using LibraryManagement.BLL;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
	public class BookController(BookService service) : Controller {
		private readonly BookService _service = service;

		public IActionResult Index() {
			return View(_service.GetAllBooks());
		}

		public IActionResult Book(int id) {
			try {
				Book selected = _service.GetBook(id);
				return View(selected);
			} catch (KeyNotFoundException ex) {
				return NotFound(ex.Message);
			}
		}

		[HttpGet]
		public IActionResult CreateBook() {
			return View();
		}

		[HttpPost]
		public IActionResult CreateBook(Book book) {
			if (ModelState.IsValid) {
				_service.CreateBook(book);
				return RedirectToAction("Index");
			}

			return View(book);
		}
	}
}
