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
        public async Task AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
    }
}
