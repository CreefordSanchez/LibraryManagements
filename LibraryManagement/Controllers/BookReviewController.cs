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

		public async Task<IActionResult> AddReview(BookReview review)
		{
			if (ModelState.IsValid)
			{
				await _service.AddReview(review);
				return RedirectToAction("Index");
			}
			return View(review);
		}
	}
}
