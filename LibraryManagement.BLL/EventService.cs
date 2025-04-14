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

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _repo.GetEventById(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ev = await _repo.GetEventById(id);
            if (ev == null)
                return false;

            await _repo.DeleteAsync(ev);
            return true;
        }

        public void CreateEvent(Event review)
        {
            _repo.CreateEvent(review);
        }
    }
}
