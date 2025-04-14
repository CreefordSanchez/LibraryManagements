using System.Security.Claims;
using LibraryManagement.BLL;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
	public class EventController(EventService service) : Controller {
		private readonly EventService _service = service;
		public IActionResult Index() {
			return View(_service.GetAllEvents());
		}

		public IActionResult GetEvent(int id) {
			return View(_service.GetEvent(id));
		}

		[HttpGet]
		public IActionResult CreateEvent() {
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			ViewBag.UserId = userId;

			return View();
		}

		[HttpPost]
		public IActionResult CreateEvent(Event events) {
			if (ModelState.IsValid) {
				_service.CreateEvent(events);
				return RedirectToAction("Index");
			}

			return View(events);
		}

	}
}
