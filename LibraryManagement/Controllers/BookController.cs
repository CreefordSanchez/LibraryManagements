﻿using LibraryManagement.BLL;
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

        public IActionResult Book(int id)
        {
            try
            {
                Book selected = _service.GetBook(id);
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
            Book? review = _service.GetBook(id);
            if (review == null)
                return NotFound();

            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleted = await _service.DeleteBook(id);
            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}