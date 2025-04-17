using System.Security.Claims;
using LibraryManagement.BLL;
using LibraryManagement.Models;
using LibraryManagement.Models.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
	public class BookReviewController(BookReviewService service, BookService bookService) : Controller {
		private readonly BookReviewService _service = service;
		private readonly BookService _bookService = bookService;
		public IActionResult Index() {
			return View(_service.GetAllBookReviews());
		}

		[Authorize(Roles = "Admin, User")]
		public IActionResult UserBookReviews() {
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			return View(_service.GetReviewsByUser(userId));
		}

		[Authorize(Roles = "Admin, User")]
		[HttpGet]
		public IActionResult CreateBookReview() {
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			ViewBag.UserId = userId;
			ViewBag.BookList = _bookService.GetAllBooks();

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

        [HttpGet]
        public IActionResult Delete(int id)
        {
            BookReview? review =  _service.GetBookReview(id);
            if (review == null)
                return NotFound();
            
            DeleteConfirmationViewModel? vm = new DeleteConfirmationViewModel
            {
                Id = review.BookReviewId,
                Title = review.Comment.Length > 50 ? review.Comment[..50] + "..." : review.Comment,
                EntityName = "Book Review",
                DeleteAction = "DeleteConfirmed",
                DeleteController = "BookReview"
            };

            return View(vm);
		}

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            bool deleted = _service.DeleteBookReview(id);
            if (!deleted)
                return NotFound();

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public IActionResult Edit(int id) {
			BookReview review = _service.GetBookReview(id);
			if (review == null) {
				return NotFound();
			}
			ViewBag.BookList = _bookService.GetAllBooks();
			return View(review);
		}

		[HttpPost]
		public IActionResult Edit(BookReview review) {
			if (ModelState.IsValid) {
				_service.EditBookReview(review);
				return RedirectToAction("Index");
			}
			ViewBag.BookList = _bookService.GetAllBooks();
			return View(review);
		}

	}
}
