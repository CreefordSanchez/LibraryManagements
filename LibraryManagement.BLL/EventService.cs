using LibraryManagement.DAL;
using LibraryManagement.Models;

namespace LibraryManagement.BLL {
	public class EventService(EventRepository repo) {
		private readonly EventRepository _repo = repo;

		public List<Event> GetAllEvents() {
			return _repo.GetAllEvents();
		}

		public Event GetEvent(int id) {
			Event? selected = _repo.GetEvent(id);
			if (selected == null) {
				throw new KeyNotFoundException("Event not found");
			}
			return selected;
		}

        public Event? GetById(int id)
        {
            return _repo.GetEvent(id);
        }

        public bool Delete(int id)
        {
            Event ev = _repo.GetEvent(id);
            if (ev == null)
                return false;

            _repo.Delete(ev);
            return true;
        }

        public void CreateEvent(Event review)
        {
            _repo.CreateEvent(review);
        }
    }
}
