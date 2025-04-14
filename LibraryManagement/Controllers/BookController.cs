using LibraryManagement.BLL;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
	public class BookController(BookService service, BookReviewService rService) : Controller {
		private readonly BookService _service = service;
		private readonly BookReviewService _reviewService = rService;

		public IActionResult Index() {
			return View(_service.GetAllBooks());
		}

		public IActionResult Book(int id) {
			ViewBag.ReviewList = _reviewService.GetReviewsByBook(id);
			return View(_service.GetBook(id));
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
