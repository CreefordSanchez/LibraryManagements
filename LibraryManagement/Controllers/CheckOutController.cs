using LibraryManagement.BLL;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
	public class CheckOutController(CheckOutService service) : Controller {
		private readonly CheckOutService _service = service;
		public IActionResult Index() {
			return View(_service.GetAllCheckOuts());
		}

		public IActionResult UserCheckOuts(string id) {
			try {
				List<CheckOut> selected = _service.GetCheckOutByUser(id);
				return View(selected);
			} catch (KeyNotFoundException ex) {
				return NotFound(ex.Message);
			}
		}

		public IActionResult DueDateCheckOuts(DateTime dueDate) {
			try {
				List<CheckOut> selected = _service.GetCheckOutByDueDate(dueDate);
				return View(selected);
			} catch (KeyNotFoundException ex) {
				return NotFound(ex.Message);
			}
		}

		[HttpGet]
        public async Task<IActionResult> Delete(int bookId, string userId)
        {
            CheckOut? checkout = await _service.GetByCompositeKeyAsync(bookId, userId);
            if (checkout == null)
                return NotFound();

            return View(checkout);
        }

		[HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int bookId, string userId)
        {
            bool deleted = await _service.DeleteCheckOutAsync(bookId, userId);
            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
