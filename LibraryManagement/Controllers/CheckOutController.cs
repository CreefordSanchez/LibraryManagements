using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
    public class CheckOutController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
