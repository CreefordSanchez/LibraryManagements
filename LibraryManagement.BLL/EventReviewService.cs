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
	}
}
 