﻿using LibraryManagement.DAL;
using LibraryManagement.Models;

namespace LibraryManagement.BLL {
	public class BookService(BookRepository repo) {
		private readonly BookRepository _repo = repo;
		public List<Book> GetAllBooks() {
			return _repo.GetAllBooks();
		}

		public Book GetBook(int id) {
			Book? selected = _repo.GetBook(id);
			if (selected == null) {
				throw new KeyNotFoundException($"No book found with ID {id}");
			}
			return selected;
		}
	}
}
