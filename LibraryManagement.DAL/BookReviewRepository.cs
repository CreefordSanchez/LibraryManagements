using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DAL {
	public class BookReviewRepository(LibraryManagementContext context) {
		private readonly LibraryManagementContext _context = context;

		public List<BookReview> GetAllBookReviews() {
			return _context.BookReviews.ToList();
		}

		public List<BookReview> GetReviewsByBook(int bookId) {
			return _context.BookReviews.Where(bk => bk.BookId == bookId).ToList();
		}

		public List<BookReview> GetReviewsByUser(string userId) {
			return _context.BookReviews.Where(bk => bk.UserId == userId).ToList();
		}

        public BookReview? GetByCompositeKeyAsync(int bookId, string userId)
        {
            return _context.BookReviews.FirstOrDefault(br => br.BookId == bookId && br.UserId ==userId);
        }

        public BookReview GetBookReview(int id)
        {
            BookReview? selected = _context.BookReviews.FirstOrDefault(b => b.BookId == id);
            return selected;
        }

        public void Delete(BookReview review)
        {
            _context.BookReviews.Remove(review);
            _context.SaveChanges();
        }

		public void CreateBookReview(BookReview review) {
			_context.BookReviews.Add(review);
			_context.SaveChanges();
		}
	}
}