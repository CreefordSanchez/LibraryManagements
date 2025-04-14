using System.Security.Claims;
using LibraryManagement.BLL;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
	public class CheckOutController(CheckOutService service, BookService bookService) : Controller {
		private readonly CheckOutService _service = service;
		private readonly BookService _bookService = bookService;

		public IActionResult Index() {
			return View(_service.GetAllCheckOuts());
		}

		public IActionResult UserCheckOuts(string id) {
			return View(_service.GetCheckOutByUser(id));
		}

		public IActionResult DueDateCheckOuts(DateTime dueDate) {
			return View(_service.GetCheckOutByDueDate(dueDate));
		}

		[HttpGet]
		public IActionResult CreateCheckOut() {
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			ViewBag.UserId = userId;
			ViewBag.BookList = _bookService.GetAllBooks();
			ViewBag.Checked = DateTime.Now;
			ViewBag.DueDate = DateTime.Now.AddDays(30);

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
