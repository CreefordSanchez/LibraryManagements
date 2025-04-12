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
        public async Task<IActionResult> Delete(int bookId, string userId)
        {
            BookReview? result = await _service.GetByCompositeKeyAsync(bookId, userId);
            if (result == null)
                return NotFound();

            return View(result);
        }

		[HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int bookId, string userId)
        {
            bool deleted = await _service.DeleteBookReviewAsync(bookId, userId);
            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
