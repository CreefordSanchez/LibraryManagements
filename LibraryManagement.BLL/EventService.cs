using LibraryManagement.DAL;
using LibraryManagement.Models;

namespace LibraryManagement.BLL {
	public class EventService(EventRepository repo) {
		private readonly EventRepository _repo = repo;

		public List<Event> GetAllEvents() {
			List<Event> events = _repo.GetAllEvents();
			if (events == null || events.Count == 0) {
				return new List<Event>();
			}
			return events;
		}

		public Event GetEvent(int id) {
			Event? selected = _repo.GetEvent(id);
			if (selected == null) {
				return new Event();
			}
			return selected;
		}

		public void CreateEvent(Event review) {
			_repo.CreateEvent(review);
		}
	}
}
