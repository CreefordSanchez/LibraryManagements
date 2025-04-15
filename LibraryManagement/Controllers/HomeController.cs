using System.Diagnostics;
using LibraryManagement.BLL;
using LibraryManagement.Models;
using LibraryManagement.Models.ModelViews;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookService _bookService;
        private readonly EventService _eventService;

		public HomeController(ILogger<HomeController> logger, BookService bSerivce, EventService eService)
        {
            _logger = logger;
			_bookService = bSerivce;
			_eventService = eService;
		}

        public IActionResult Index()
        {
			List<Book> books = _bookService.GetAllBooks();
			IEnumerable<Event> events = _eventService.GetAllEvents().Where(e => e.Date > DateOnly.FromDateTime(DateTime.Now) && e.Time > TimeOnly.FromDateTime(DateTime.Now));
			var query = books.Join(events, book => book.BookId, e => e.EventId, (book, e) => new { book, e }).ToList();
			List<BookAndEvent> bookAndEvents = new List<BookAndEvent>();
            foreach (var item in query) {
                BookAndEvent bookAndEvent = new BookAndEvent {
                    BookId = item.book.BookId,
					BookTitle = item.book.Title,
                    BookAuthor = item.book.Author,
                    BookGenre = item.book.Genre,
                    BookPublished = item.book.Published,
					EventId = item.e.EventId,
					EventTitle = item.e.Title,
                    EventDate = item.e.Date,
                    EventTime = item.e.Time,
                    EventLocation = item.e.Location,
                };
            }
			return View(bookAndEvents);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
