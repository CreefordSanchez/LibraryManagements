﻿using System.Security.Claims;
using LibraryManagement.BLL;
using LibraryManagement.Models;
using LibraryManagement.Models.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

		[HttpGet]
		public IActionResult Delete(int id) {
			EventReview? review = _service.GetById(id);
			if (review == null)
				return NotFound();

            DeleteConfirmationViewModel? vm = new DeleteConfirmationViewModel
            {
                Id = review.EventReviewId,
                Title = review.Comment.Length > 50 ? review.Comment[..50] + "..." : review.Comment,
                EntityName = "Event Review",
                DeleteAction = "DeleteConfirmed",
                DeleteController = "EventReview"
            };

            return View(vm);
        }

		[HttpPost]
		public IActionResult DeleteConfirmed(int id) {
			bool deleted = _service.Delete(id);
			if (!deleted)
				return NotFound();

			return RedirectToAction(nameof(Index));
		}

		[Authorize(Roles = "Admin, User")]
		[HttpGet]
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ViewBag.UserId = userId;
            ViewBag.EventList = _eventService.GetAllEvents();

            return View(review);
		}

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
		public IActionResult Edit(int id) {
			EventReview? review = _service.GetById(id);
			if (review == null) {
				return NotFound();
			}
            string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (User.IsInRole("User") && review.UserId != currentUserId) {
				return Forbid();
			}
            ViewBag.EventList = _eventService.GetAllEvents();
			return View(review);
		}

		[Authorize(Roles = "Admin, User")]
		[HttpPost]
		public IActionResult Edit(EventReview review) {
			string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (!User.IsInRole("Admin") && review.UserId != currentUserId) { 
				return Forbid();
			}
            if (ModelState.IsValid) {
				_service.EditEventReview(review);
				return RedirectToAction("Index");
			}
			ViewBag.EventList = _eventService.GetAllEvents();
			return View(review);
		}
	}
}