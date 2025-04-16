using System.Security.Claims;
using LibraryManagement.BLL;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
	public class BookReviewController(BookReviewService service, BookService bookService) : Controller {
		private readonly BookReviewService _service = service;
		private readonly BookService _bookService = bookService;
		public IActionResult Index()
		{
			return View(_service.GetAllBookReviews());
		}

		public IActionResult BookReviews(int id) {
			return View(_service.GetReviewsByBook(id));
		}

		public IActionResult UserBookReviews(string id) {
			return View(_service.GetReviewsByUser(id));
		}

        public IActionResult CreateBookReview()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ViewBag.UserId = userId;
            ViewBag.BookList = _bookService.GetAllBooks();

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            BookReview? review =  _service.GetBookReview(id);
            if (review == null)
                return NotFound();

			return View();
		}

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            bool deleted = _service.DeleteBookReview(id);
            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
