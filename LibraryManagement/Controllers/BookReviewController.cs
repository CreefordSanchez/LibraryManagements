using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
    public class BookReviewController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
