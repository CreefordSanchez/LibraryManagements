using LibraryManagement.Models;

namespace LibraryManagement.DAL {
	public class EventRepository(LibraryManagementContext context) {
		private readonly LibraryManagementContext _context = context;

		public List<Event> GetAllEvents() {
			return _context.Events.ToList();
		}

		public Event GetEvent(int id) {
			Event? selected = _context.Events.FirstOrDefault(e => e.EventId == id);
			return selected;
		}
	}
}
