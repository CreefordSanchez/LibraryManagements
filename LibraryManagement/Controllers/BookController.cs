using LibraryManagement.BLL;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
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
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(Book book)
		{
			if (ModelState.IsValid)
			{
				await _service.AddBook(book);
				return RedirectToAction("Index");
			}
			return View(book);
		}
	}
}
