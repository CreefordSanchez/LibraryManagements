using LibraryManagement.BLL;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
    public class EventReviewController(EventReviewService service) : Controller {
		private readonly EventReviewService _service = service;
        public List<EventReview> GetAllEventReviews() {
			return _service.GetAllEventReviews();
		}

        public List<EventReview> GetEventReviews(int eventId) {
			return _service.GetReviewsByEvent(eventId);
		}
		public IActionResult Index() {
            return View(GetAllEventReviews());
        }

        public IActionResult EventReview(int id) {
            return View(GetEventReviews(id));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int eventId, string userId)
        {
            EventReview? result = await _service.GetByCompositeKeyAsync(eventId, userId);
            if (result == null)
                return NotFound();

            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int eventId, string userId)
        {
            bool deleted = await _service.DeleteBookReviewAsync(eventId, userId);
            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}