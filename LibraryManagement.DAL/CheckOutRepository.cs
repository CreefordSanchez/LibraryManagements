using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DAL {
	public class CheckOutRepository(LibraryManagementContext context) {
		private readonly LibraryManagementContext _context = context;

		public List<CheckOut> GetAllCheckOuts() {
			return _context.CheckOuts.ToList();
		}

		public List<CheckOut> GetCheckOutByUser(string id) {
			return _context.CheckOuts.Where(x => x.UserId == id).ToList();
		}

		public List<CheckOut> GetCheckOutByDueDate(DateTime dueDate) {
			return _context.CheckOuts.Where(x => x.DueDate == dueDate).ToList();
		}

        public CheckOut? GetByCompositeKey(int bookId, string userId)
        {
            return _context.CheckOuts.FirstOrDefault(c => c.BookId == bookId && c.UserId == userId);
        }

        public void Delete(CheckOut checkout)
        {
            _context.CheckOuts.Remove(checkout);
            _context.SaveChanges();
        }

        public void CreateCheckOut(CheckOut check)
        {
            _context.CheckOuts.Add(check);
            _context.SaveChanges();
        }
    }
}