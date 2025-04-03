using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
    public class BookController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
