using LibraryManagement.BLL;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            EventReview? review = _service.GetById(id);
            if (review == null)
                return NotFound();

            return View(review);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            bool deleted = _service.Delete(id);
            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateEventReview()
        {
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
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            EventReview? review = _service.GetById(id);
            if (review == null) {
                return NotFound();
            }
            ViewBag.EventList = _eventService.GetAllEvents();
            return View(review);
        }

        [HttpPost]
        public IActionResult Edit(EventReview review) {
            if (ModelState.IsValid) {
                _service.EditEventReview(review);
                return RedirectToAction("Index");
            }
            ViewBag.EventList = _eventService.GetAllEvents();
            return View(review);
        }
    }
}