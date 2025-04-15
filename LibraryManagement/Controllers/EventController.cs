using System.Security.Claims;
using LibraryManagement.BLL;
using LibraryManagement.Models;
using LibraryManagement.Models.ModelViews;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
	public class EventController(EventService service, EventReviewService erService) : Controller {
		private readonly EventService _service = service;
		private readonly EventReviewService _eventReviewService = erService;
		public IActionResult Index() {
			return View(_service.GetAllEvents());
		}

		[HttpGet]
		public IActionResult Delete(int id) {
			Event? ev = _service.GetById(id);
			if (ev == null)
				return NotFound();

			return View(ev);
		}

		[HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            bool deleted = _service.Delete(id);
            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
		public IActionResult Event(int id) {
			List<EventReview> eventReviews = _eventReviewService.GetReviewsByEvent(id);
			List<Event> events = _service.GetAllEvents();
			var query = events.Join(eventReviews, e => e.EventId, review => review.EventId, (e, review) => new { e, review }).Where(x => x.e.EventId == id)
				.ToList();
			List<EventAndReview> eventAndReview = new List<EventAndReview>();
			foreach (var item in query) {
				EventAndReview eventAndReviews = new EventAndReview {
					Title = item.e.Title,
					Date = item.e.Date,
					Time = item.e.Time,
					Location = item.e.Location,
					Description = item.e.Description,
					Rating = item.review.Rating,
					Comment = item.review.Comment
				};
				eventAndReview.Add(eventAndReviews);
			}
			return View();
		}

		[HttpGet]
		public IActionResult CreateEvent() {
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			ViewBag.UserId = userId;

			return View();
		}

        [HttpPost]
        public IActionResult CreateEvent(Event events)
        {
            if (ModelState.IsValid)
            {
                _service.CreateEvent(events);
                return RedirectToAction("Index");
            }

			return View(events);
		}

	}
}
