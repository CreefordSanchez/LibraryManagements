using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
namespace LibraryManagement.DAL {
	public class BookRepository(LibraryManagementContext context) {
		private readonly LibraryManagementContext _context = context;

		public List<Book> GetAllBooks() {
			return _context.Books
				.Include(b => b.BookReviews)
				.Include(b => b.CheckOut)
				.ToList();
		}

		public Book GetBook(int id) {
			Book? selected = _context.Books
				.Include(b => b.BookReviews)
				.Include(b => b.CheckOut)
				.FirstOrDefault(b => b.BookId == id);
			return selected;
		}

		public void Delete(Book book)
		{
			_context.Books.Remove(book);
			_context.SaveChanges();
		}

        public void CreateBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }
    }
}
