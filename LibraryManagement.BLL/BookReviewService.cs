using LibraryManagement.DAL;
using LibraryManagement.Models;
namespace LibraryManagement.BLL {
	public class BookReviewService(BookReviewRepository repo) {
		private readonly BookReviewRepository _repo = repo;

		public List<BookReview> GetAllBookReviews() {
			return _repo.GetAllBookReviews();
		}

		public List<BookReview> GetReviewsByBook(int bookId) {
			List<BookReview>? selected = _repo.GetReviewsByBook(bookId);
			if (selected == null || selected.Count == 0) {
				return new List<BookReview>();
			}
			return selected;
		}

		public List<BookReview> GetReviewsByUser(string userId) {
			List<BookReview>? selected = _repo.GetReviewsByUser(userId);
			if (selected == null || selected.Count == 0) {
				return new List<BookReview>();
			}
			return selected;
		}

		public void CreateBookReview(BookReview review) {
			_repo.CreateBookReview(review);
		}
	}
}
