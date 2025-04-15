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
			if (selected == null || selected.Count == 0) {
				return new List<CheckOut>();
			}
			return selected;
		}
		public List<CheckOut> GetCheckOutByDueDate(DateOnly dueDate) {
			List<CheckOut>? selected = _repo.GetCheckOutByDueDate(dueDate);
			if (selected == null || selected.Count == 0) {
				return new List<CheckOut>();
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
