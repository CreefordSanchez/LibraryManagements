﻿using LibraryManagement.Models;

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

	}
}
