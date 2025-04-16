using System.Diagnostics;
using LibraryManagement.BLL;
using LibraryManagement.Models;
using LibraryManagement.Models.ModelViews;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
	public class HomeController : Controller {
		private readonly ILogger<HomeController> _logger;
		private readonly BookService _bookService;
		private readonly EventService _eventService;

		public HomeController(ILogger<HomeController> logger, BookService bSerivce, EventService eService) {
			_logger = logger;
			_bookService = bSerivce;
			_eventService = eService;
		}

		public IActionResult Index() {
			IEnumerable<BookAndEvent> books = _bookService.GetAllBooks().Select(book => new BookAndEvent {
				ID = book.BookId,
				Title = book.Title,
				Date = book.Published,
				Time = null,
				AuthOrLocation = book.Author,
				Genre = book.Genre,
				Source = "Book"
			});
			IEnumerable<BookAndEvent> events = _eventService.GetAllEvents().Select(e => new BookAndEvent {
				ID = e.EventId,
				Title = e.Title,
				Date = e.Date,
				Time = e.Time,
				AuthOrLocation = e.Location,
				Genre = null,
				Source = "Event"
			}).Where(e => e.Date > DateOnly.FromDateTime(DateTime.Now) && e.Date < DateOnly.FromDateTime(DateTime.Now.AddDays(14)));

			IEnumerable<BookAndEvent> bookAndEvents = books.Concat(events);
			return View(bookAndEvents);
		}

		public IActionResult Privacy() {
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
