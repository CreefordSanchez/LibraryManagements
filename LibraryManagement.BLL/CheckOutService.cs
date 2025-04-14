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
		public List<CheckOut> GetCheckOutByDueDate(DateTime dueDate) {
			List<CheckOut>? selected = _repo.GetCheckOutByDueDate(dueDate);
			if (selected == null || selected.Count == 0) {
				return new List<CheckOut>();
			}
			return selected;
		}

		public void CreateCheckOut(CheckOut check) {
			_repo.CreateCheckOut(check);
		}
	}
}
