using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DAL {
	public class CheckOutRepository(LibraryManagementContext context) {
		private readonly LibraryManagementContext _context = context;

		public List<CheckOut> GetAllCheckOuts() {
			return _context.CheckOuts.Include(c => c.Book).ToList();
		}

		public List<CheckOut> GetCheckOutByUser(string id) {
			return _context.CheckOuts.Where(x => x.UserId == id).Include(c => c.Book).ToList();
		}

		public List<CheckOut> GetCheckOutByDueDate(DateOnly dueDate) {
			return _context.CheckOuts.Where(x => x.DueDate == dueDate).Include(c => c.Book).ToList();
		}

        public CheckOut? GetByCompositeKey(int bookId, string userId)
        {
            return _context.CheckOuts.FirstOrDefault(c => c.BookId == bookId && c.UserId == userId);
        }

        public void CreateCheckOut(CheckOut check)
        {
            _context.CheckOuts.Add(check);
            _context.SaveChanges();
        }

        public void Edit(CheckOut checkout) {
            _context.CheckOuts.Update(checkout);
            _context.SaveChanges();
        }
    }
}