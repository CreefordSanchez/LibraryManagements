using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DAL {
	public class EventRepository(LibraryManagementContext context) {
		private readonly LibraryManagementContext _context = context;

		public List<Event> GetAllEvents() {
			return _context.Events.Include(e => e.EventReviews).ToList();
		}

		public Event GetEvent(int id) {
			Event? selected = _context.Events.Include(e => e.EventReviews).FirstOrDefault(e => e.EventId == id);
			return selected;
		}

		public void CreateEvent(Event events) {
			_context.Events.Add(events);
			_context.SaveChanges();
		}
	}
}
