﻿using System.Security.Claims;
using LibraryManagement.BLL;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
    public class BookController(BookService service) : Controller
    {
        private readonly BookService _service = service;

        public IActionResult Index()
        {
            return View(_service.GetAllBooks());
        }

		public IActionResult Book(int id) {
			return View(_service.GetBook(id));
		}

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Book? review = _service.GetBook(id);
            if (review == null)
                return NotFound();

            return View(review);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            bool deleted = _service.DeleteBook(id);
            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult CreateBookReview()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ViewBag.UserId = userId;
            ViewBag.BookList = _service.GetAllBooks();

            return View();
        }

		[HttpPost]
		public IActionResult CreateBook(Book book) {
			if (ModelState.IsValid) {
				_service.CreateBook(book);
				return RedirectToAction("Index");
			}

			return View(book);
		}
	}
}
