using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
    public class EventReviewController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
