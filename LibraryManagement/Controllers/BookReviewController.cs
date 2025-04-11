using LibraryManagement.BLL;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
	public class BookReviewController(BookReviewService service) : Controller {
		private readonly BookReviewService _service = service;
		public IActionResult Index() {
			return View(_service.GetAllBookReviews());
		}

		public IActionResult BookReviews(int id) {
			try {
				List<BookReview> selected = _service.GetReviewsByBook(id);
				return View(selected);
			} catch (KeyNotFoundException ex) {
				return NotFound(ex.Message);
			}
		}

		public IActionResult UserBookReviews(string id) {
			try {
				List<BookReview> selected = _service.GetReviewsByUser(id);
				return View(selected);
			} catch (KeyNotFoundException ex) {
				return NotFound(ex.Message);
			}
		}

		[HttpGet]
		public IActionResult CreateBookReview() {
			return View();
		}

		[HttpPost]
		public IActionResult CreateBookReview(BookReview review) {
			if (ModelState.IsValid) {
				_service.CreateBookReview(review);
				return RedirectToAction("Index");
			}

			return View(review);
		}
	}
}
