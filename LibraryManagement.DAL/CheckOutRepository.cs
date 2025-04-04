using LibraryManagement.Models;

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
	}
}
