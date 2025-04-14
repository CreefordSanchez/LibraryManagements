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

        public EventReview GetById(int id)
        {
            return _repo.GetById(id);
        }

        public bool Delete(int id)
        {
            EventReview? review = _repo.GetById(id);
            if (review == null)
                return false;

            _repo.Delete(review);
            return true;
        }

        public void CreateEventReview(EventReview review)
        {
            _repo.CreateEventReview(review);
        }
    }
}
 