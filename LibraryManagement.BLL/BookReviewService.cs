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

        public BookReview GetBookReview(int id)
        {
            BookReview? selected = _repo.GetBookReview(id);
            if (selected == null)
            {
                throw new KeyNotFoundException($"No book review found with ID {id}");
            }
            return selected;
        }

        public BookReview GetBook(int id)
        {
            BookReview? selected = _repo.GetBookReview(id);
            if (selected == null)
            {
                throw new KeyNotFoundException($"No book found with ID {id}");
            }
            return selected;
        }

        public bool DeleteBookReview(int id)
        {
            BookReview? review = _repo.GetBookReview(id);
            if (review == null)
                return false;

            _repo.Delete(review);
            return true;
        }

        public void CreateBookReview(BookReview review)
        {
            _repo.CreateBookReview(review);
        }

        public void EditBookReview(BookReview review) {
            _repo.Edit(review);
        }
    }
}
