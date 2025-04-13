﻿using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<BookReview?> GetByCompositeKeyAsync(int bookId, string userId)
        {
            return await _context.BookReviews
                .FirstOrDefaultAsync(br => br.BookId == bookId && br.UserId ==userId);
        }

        public BookReview GetBookReview(int id)
        {
            BookReview? selected = _context.BookReviews.FirstOrDefault(b => b.BookId == id);
            return selected;
        }

        public async Task DeleteAsync(BookReview review)
        {
            _context.BookReviews.Remove(review);
            await _context.SaveChangesAsync();
        }
	}
}