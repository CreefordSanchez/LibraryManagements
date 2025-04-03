using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
    public class EventController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
