using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Event?> GetEventById(int id)
        {
            return await _context.Events
                .Include(e => e.EventReviews)
                .FirstOrDefaultAsync(e => e.EventId == id);
        }

        public async Task DeleteAsync(Event ev)
        {
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
        }

        public void CreateEvent(Event events)
        {
            _context.Events.Add(events);
            _context.SaveChanges();
        }
    }
}