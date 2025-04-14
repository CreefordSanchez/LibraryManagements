using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

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

		public void CreateBookReview(BookReview review) {
			_context.BookReviews.Add(review);
			_context.SaveChanges();
		}
	}
}
