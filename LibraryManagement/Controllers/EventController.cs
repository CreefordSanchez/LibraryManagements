﻿using LibraryManagement.BLL;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
	public class EventController(EventService service) : Controller {
		private readonly EventService _service = service;
		public IActionResult Index() {
			return View(_service.GetAllEvents());
		}

		public IActionResult GetEvent(int id) {
			try {
				Event selected = _service.GetEvent(id);
				return View(selected);
			} catch (KeyNotFoundException ex) {
				return NotFound(ex.Message);
			}
		}

		[HttpGet]
		public IActionResult CreateEvent() {
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
