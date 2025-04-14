using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DAL {
    public class EventReviewRepository(LibraryManagementContext context) {
        private readonly LibraryManagementContext _context = context;

        public List<EventReview> GetAllEventReviews() {
			return _context.EventReviews.ToList();
		}

		public List<EventReview> GetReviewsByEvent(int eventId) {
			return _context.EventReviews.Where(er => er.EventId == eventId).ToList();
		}

        public async Task<EventReview?> GetByIdAsync(int id)
        {
            return await _context.EventReviews
                .Include(r => r.Event)
                .FirstOrDefaultAsync(r => r.EventReviewId == id);
        }

        public async Task DeleteAsync(EventReview review)
        {
            _context.EventReviews.Remove(review);
            await _context.SaveChangesAsync();
        }

        public void CreateEventReview(EventReview review)
        {
            _context.EventReviews.Add(review);
            _context.SaveChanges();
        }
    }
}