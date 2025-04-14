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
        public async Task<IActionResult> Delete(int id)
        {
            EventReview? review = await _service.GetByIdAsync(id);
            if (review == null)
                return NotFound();

            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _service.DeleteAsync(id); // it wouldn't let me put a boolean as the type, most likely an error on my part -Mat
            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult CreateEventReview()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ViewBag.UserId = userId;
            ViewBag.EventList = _eventService.GetAllEvents();

            return View();
        }

        [HttpPost]
        public IActionResult CreateEventReview(EventReview review)
        {
            if (ModelState.IsValid)
            {
                _service.CreateEventReview(review);
                return RedirectToAction("Index");
            }

            return View(review);
        }
    }
}