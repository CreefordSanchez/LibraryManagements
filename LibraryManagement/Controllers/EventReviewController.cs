using LibraryManagement.BLL;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagement.Controllers {
    public class EventReviewController(EventReviewService service, EventService eventService) : Controller {
		private readonly EventReviewService _service = service;
        private readonly EventService _eventService = eventService;
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
<<<<<<< HEAD
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
=======
        public IActionResult CreateEventReview() {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ViewBag.UserId = userId;
            ViewBag.EventList = _eventService.GetAllEvents();

            return View();
        }

        [HttpPost]
        public IActionResult CreateEventReview(EventReview review) {
            if (ModelState.IsValid) {
                _service.CreateEventReview(review);
                return RedirectToAction("Index");
            }

            return View(review);
>>>>>>> 09f740746b6e048dcf17af626ddcd0823cdb27ab
        }
    }
}