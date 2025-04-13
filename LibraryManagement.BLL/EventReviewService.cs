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

<<<<<<< HEAD
        public async Task<bool> DeleteBookReviewAsync(int eventId, string userId)
        {
            EventReview? result = await _repo.GetByCompositeKeyAsync(eventId, userId);
            if (result == null)
                return false;

            await _repo.DeleteAsync(result);
            return true;
        }

        public async Task<EventReview?> GetByCompositeKeyAsync(int eventId, string userId)
        {
            return await _repo.GetByCompositeKeyAsync(eventId, userId);
=======
        public void CreateEventReview(EventReview review) {
            _repo.CreateEventReview(review);
>>>>>>> 09f740746b6e048dcf17af626ddcd0823cdb27ab
        }
    }
}
 