using LibraryManagement.Models;
namespace LibraryManagement.DAL {
	public class BookRepository(LibraryManagementContext context) {
		private readonly LibraryManagementContext _context = context;

		public List<Book> GetAllBooks() {
			return _context.Books.ToList();
		}

		public Book GetBook(int id) {
			Book? selected = _context.Books.FirstOrDefault(b => b.BookId == id);
			return selected;
		}

		public async Task Delete(Book book)
		{
			_context.Books.Remove(book);
			await _context.SaveChangesAsync();
		}

        public void CreateBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }
    }
}
