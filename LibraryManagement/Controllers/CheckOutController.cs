using System.Security.Claims;
using LibraryManagement.BLL;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
	public class CheckOutController(CheckOutService service, BookService bookService) : Controller {
		private readonly CheckOutService _service = service;
		private readonly BookService _bookService = bookService;

		[Authorize(Roles = "Admin")]
		public IActionResult Index() {
			return View(_service.GetAllCheckOuts());
		}

		[Authorize(Roles = "User")]
		public IActionResult UserCheckOuts(string id) {
			// Will need to be able to tell who is logged in
			return View(_service.GetCheckOutByUser(id));
		}

		[Authorize(Roles = "Admin")]
		public IActionResult DueDateCheckOuts() {
			DateOnly today = DateOnly.FromDateTime(DateTime.Today);
			return View(_service.GetCheckOutByDueDate(today));
		}

		[HttpGet]
		public IActionResult CreateCheckOut() {
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			ViewBag.UserId = userId;
			ViewBag.BookList = _bookService.GetAllBooks();
			ViewBag.Checked = DateOnly.FromDateTime(DateTime.Today);
			ViewBag.DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));

			return View();
		}

		[HttpPost]
		public IActionResult CreateCheckOut(CheckOut checkOut) {
			if (ModelState.IsValid) {
				_service.CreateCheckOut(checkOut);
				return RedirectToAction("Index");
			}

			return View(checkOut);
		}
	}
}
