using System.Security.Claims;
using LibraryManagement.BLL;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
	public class BookReviewController(BookReviewService service, BookService bookService) : Controller
	{
		private readonly BookReviewService _service = service;
		private readonly BookService _bookService = bookService;
		public IActionResult Index()
		{
			return View(_service.GetAllBookReviews());
		}

		public IActionResult BookReviews(int id)
		{
			try
			{
				List<BookReview> selected = _service.GetReviewsByBook(id);
				return View(selected);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}

		public IActionResult UserBookReviews(string id)
		{
			try
			{
				List<BookReview> selected = _service.GetReviewsByUser(id);
				return View(selected);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpGet]
        public IActionResult Delete(int id)
        {
            BookReview? review =  _service.GetBookReview(id);
            if (review == null)
                return NotFound();

            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleted = await _service.DeleteBookReview(id);
            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
