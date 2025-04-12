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

        public async Task<EventReview?> GetByCompositeKeyAsync(int eventId, string userId)
        {
            return await _context.EventReviews
                .FirstOrDefaultAsync(er => er.EventId == eventId && er.UserId == userId);
        }

        public async Task DeleteAsync(EventReview review)
        {
            _context.EventReviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}
