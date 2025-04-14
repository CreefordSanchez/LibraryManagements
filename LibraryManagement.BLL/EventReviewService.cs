using LibraryManagement.DAL;
using LibraryManagement.Models;

namespace LibraryManagement.BLL {
	public class EventReviewService(EventReviewRepository repo) {
		private readonly EventReviewRepository _repo = repo;

		public List<EventReview> GetAllEventReviews() {
			List<EventReview> reviews = _repo.GetAllEventReviews();
			if (reviews == null || reviews.Count == 0) {
				return new List<EventReview>();
			}
			return reviews;
		}
		public List<EventReview> GetReviewsByEvent(int eventId) {
			List<EventReview> reviews = _repo.GetReviewsByEvent(eventId);
			if (reviews == null || reviews.Count == 0) {
				return new List<EventReview>();
			}
			return reviews;
		}

		public void CreateEventReview(EventReview review) {
			_repo.CreateEventReview(review);
		}
	}
}
