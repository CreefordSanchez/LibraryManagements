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
        public IActionResult CreateEventReview() {
            return View();
        }

        [HttpPost]
        public IActionResult CreateEventReview(EventReview review) {
            if (ModelState.IsValid) {
                _service.CreateEventReview(review);
                return RedirectToAction("Index");
            }

            return View(review);
        }
    }
}
