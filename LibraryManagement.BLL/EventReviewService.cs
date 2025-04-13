using LibraryManagement.DAL;
using LibraryManagement.Models;

namespace LibraryManagement.BLL {
    public class EventReviewService(EventReviewRepository repo) {
        private readonly EventReviewRepository _repo = repo;

        public List<EventReview> GetAllEventReviews() {
			return _repo.GetAllEventReviews();
		}
		public List<EventReview> GetReviewsByEvent(int eventId) {
			return _repo.GetReviewsByEvent(eventId);
		}

        public async Task<EventReview?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            EventReview? review = await _repo.GetByIdAsync(id);
            if (review == null)
                return false;

            await _repo.DeleteAsync(review);
            return true;
        }
    }
}
 