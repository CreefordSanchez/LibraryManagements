using LibraryManagement.DAL;
using LibraryManagement.Models;

namespace LibraryManagement.BLL {
	public class CheckOutService(CheckOutRepository repo) {
		private readonly CheckOutRepository _repo = repo;

		public List<CheckOut> GetAllCheckOuts() {
			return _repo.GetAllCheckOuts();
		}
		public List<CheckOut> GetCheckOutByUser(string id) {
			List<CheckOut>? selected = _repo.GetCheckOutByUser(id);
			if (selected == null) {
				throw new KeyNotFoundException($"No CheckOuts found with User Id being {id}");
			}
			return selected;
		}
		public List<CheckOut> GetCheckOutByDueDate(DateTime dueDate) {
			List<CheckOut> selected = _repo.GetCheckOutByDueDate(dueDate);
			if (selected == null || selected.Count == 0) {
				throw new KeyNotFoundException("No CheckOuts found for the given due date");
			}
			return selected;
		}

        public CheckOut? GetByCompositeKey(int bookId, string userId)
        {
            return _repo.GetByCompositeKey(bookId, userId);
        }

        public bool DeleteIfReturned(int bookId, string userId)
        {
            CheckOut? checkout = _repo.GetByCompositeKey(bookId, userId);
            if (checkout == null || !checkout.IsReturned)
                return false;

            _repo.Delete(checkout);
            return true;
        }

        public void CreateCheckOut(CheckOut check)
        {
            _repo.CreateCheckOut(check);
        }
    }
}
