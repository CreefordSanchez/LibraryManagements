using System.Security.Claims;
using LibraryManagement.BLL;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Controllers {
	public class CheckOutController(CheckOutService service, BookService bookService, CheckOutService checkService, UserManager<IdentityUser> userManager) : Controller {
		private readonly CheckOutService _service = service;
		private readonly BookService _bookService = bookService;
		private readonly CheckOutService _checkService = checkService;
        private readonly UserManager<IdentityUser> _userManager = userManager;

        [Authorize]
		public IActionResult Index() {
			if (User.IsInRole("Admin")) {
				return View(_service.GetAllCheckOuts());
			} else {
				return RedirectToAction("UserCheckOuts");
			}
		}

		[Authorize(Roles = "User")]
		public IActionResult UserCheckOuts() {
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			// Will need to be able to tell who is logged in
			return View(_service.GetCheckOutByUser(userId));
		}

        [Authorize(Roles = "Admin")]
        public IActionResult DueDateCheckOuts()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            return View(_service.GetCheckOutByDueDate(today));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateCheckOut() {
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			List<int> notReturnedId = _checkService.GetAllCheckOuts()
			   .Where(co => co.IsReturned == false)
			   .Select(co => co.BookId)
			   .ToList();

			ViewBag.UserList = _userManager.Users
				.Select(u => u.Id)
				.ToList();
			ViewBag.Checked = DateOnly.FromDateTime(DateTime.Today);
			ViewBag.DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
			ViewBag.BookList = _bookService.GetAllBooks()
				.Where(b => !notReturnedId.Contains(b.BookId));

			return View();
		}

		[HttpPost]
		public IActionResult CreateCheckOut(CheckOut checkOut) {
			if (ModelState.IsValid) {
				_service.CreateCheckOut(checkOut);
				return RedirectToAction("Index");
			}

            List<int> notReturnedId = _checkService.GetAllCheckOuts()
               .Where(co => co.IsReturned == false)
               .Select(co => co.BookId)
               .ToList();

            ViewBag.Checked = DateOnly.FromDateTime(DateTime.Today);
            ViewBag.DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
            ViewBag.UserList = _userManager.Users
                .Select(u => u.Id)
                .ToList();

            ViewBag.BookList = _bookService.GetAllBooks()
                .Where(b => !notReturnedId.Contains(b.BookId));
            return View(checkOut);
		}

		[HttpGet]
		public IActionResult Edit(int bookId, string userId) {
			CheckOut? checkout = _service.GetByCompositeKey(bookId, userId);
			if (checkout == null) {
				return NotFound();
			}
			ViewBag.BookList = _bookService.GetAllBooks();
			return View(checkout);
		}

		[HttpPost]
		public IActionResult Edit(CheckOut checkout) {
			if (ModelState.IsValid) {
				_service.EditCheckOut(checkout);
				return RedirectToAction("Index");
			}
			ViewBag.BookList = _bookService.GetAllBooks();
			return View(checkout);
		}
	}
}
