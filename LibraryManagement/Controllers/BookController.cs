using LibraryManagement.BLL;
using LibraryManagement.Models;
using LibraryManagement.Models.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
	public class BookController(BookService service, BookReviewService rService) : Controller {
		private readonly BookService _service = service;
		private readonly BookReviewService _reviewService = rService;

		public IActionResult Index() {
			return View(_service.GetAllBooks());
		}

		public IActionResult Book(int id) {
			List<BookReview> bookReviews = _reviewService.GetReviewsByBook(id);
			List<Book> books = _service.GetAllBooks();
			var query = books.Join(bookReviews, book => book.BookId, review => review.BookId, (book, review) => new { book, review }).Where(x => x.book.BookId == id)
				.ToList();
			ViewBag.Book = books.FirstOrDefault(b => b.BookId == id);
			List<BookAndReview> bookAndReviews = new List<BookAndReview>();
			foreach (var item in query) {
				BookAndReview bookAndReview = new BookAndReview {
					Title = item.book.Title,
					Author = item.book.Author,
					Genre = item.book.Genre,
					Picture = item.book.Picture,
					Published = item.book.Published.ToShortDateString(),
					UserId = item.review.UserId,
					Rating = item.review.Rating,
					Comment = item.review.Comment
				};
				bookAndReviews.Add(bookAndReview);
			}
			return View(bookAndReviews);
		}

		[HttpGet]
		public IActionResult Delete(int id) {
			Book? book = _service.GetBook(id);
			if (book == null)
				return NotFound();

            DeleteConfirmationViewModel? vm = new DeleteConfirmationViewModel
            {
                Id = book.BookId,
                Title = book.Title.Length > 50 ? book.Title[..50] + "..." : book.Title,
                EntityName = "Book",
                DeleteAction = "DeleteConfirmed",
                DeleteController = "Book"
            };

            return View(vm);
        }

		[HttpPost]
		public IActionResult DeleteConfirmed(int id) {
			bool deleted = _service.DeleteBook(id);
			if (!deleted)
				return NotFound();

			return RedirectToAction("Index");
		}

		[Authorize(Roles = "Admin")]
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
