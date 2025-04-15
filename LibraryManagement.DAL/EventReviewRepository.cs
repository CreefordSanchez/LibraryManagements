using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DAL {
	public class EventReviewRepository(LibraryManagementContext context) {
		private readonly LibraryManagementContext _context = context;

		public List<EventReview> GetAllEventReviews() {
			return _context.EventReviews.Include(er => er.Event).ToList();
		}

		public List<EventReview> GetReviewsByEvent(int eventId) {
			return _context.EventReviews.Where(er => er.EventId == eventId).ToList();
		}

		public void CreateEventReview(EventReview review) {
			_context.EventReviews.Add(review);
			_context.SaveChanges();
		}
	}
}
