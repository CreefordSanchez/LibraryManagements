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
		private readonly BookReviewService _bookReviewService;

		public HomeController(ILogger<HomeController> logger, BookService bSerivce, EventService eService, BookReviewService brService) {
			_logger = logger;
			_bookService = bSerivce;
			_eventService = eService;
			_bookReviewService = brService;
		}

		private List<Book> Popular() {
			return _bookService.GetAllBooks()
			.Select(book => new {
				Book = book,
				HighRatedReviewCount = _bookReviewService.GetAllBookReviews()
					//Ensure the review is above a 3
					.Count(r => r.BookId == book.BookId && r.Rating >= 3)
			})
			//Ensure each book on this list HAS at least one review
			.Where(b => b.HighRatedReviewCount > 0)
			.OrderByDescending(b => b.HighRatedReviewCount)
			//Take the top 3 results
			.Take(3)
			.Select(b => b.Book)
			.ToList();
		}

		public IActionResult Index() {
			IEnumerable<BookAndEvent> books = Popular()
				.Select(book => new BookAndEvent {
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
