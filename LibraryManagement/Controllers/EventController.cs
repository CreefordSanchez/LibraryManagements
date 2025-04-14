using System.Security.Claims;
using LibraryManagement.BLL;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
    public class EventController(EventService service) : Controller
    {
        private readonly EventService _service = service;
        public IActionResult Index()
        {
            return View(_service.GetAllEvents());
        }

        public IActionResult GetEvent(int id)
        {
            try
            {
                Event selected = _service.GetEvent(id);
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
