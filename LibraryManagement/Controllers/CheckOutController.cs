﻿using System.Security.Claims;
using LibraryManagement.BLL;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
	public class CheckOutController(CheckOutService service, BookService bookService, CheckOutService checkService) : Controller {
		private readonly CheckOutService _service = service;
		private readonly BookService _bookService = bookService;
		private readonly CheckOutService _checkService = checkService;

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
        public IActionResult Delete(int bookId, string userId)
        {
            CheckOut checkout = _service.GetByCompositeKey(bookId, userId);
            if (checkout == null)
                return NotFound();

            if (!checkout.IsReturned)
            {
                TempData["Error"] = "Cannot delete a checkout that has not been returned.";
                return RedirectToAction(nameof(Index));
            } 
            else
            {
                _service.DeleteIfReturned(bookId, userId);
            }

            return View(checkout);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int bookId, string userId)
        {
            bool deleted = _service.DeleteIfReturned(bookId, userId);
            if (!deleted)
            {
                TempData["Error"] = "Cannot delete a checkout that has not been returned.";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public IActionResult CreateCheckOut() {
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			 List<int> notReturnedId = _checkService.GetAllCheckOuts()
				.Where(co => co.IsReturned == false)
				.Select(co => co.BookId)
				.ToList();				

			ViewBag.UserId = userId;
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
