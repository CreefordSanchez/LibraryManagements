namespace LibraryManagement.Models.ModelViews {
	public class BookAndEvent {

		public int ID { get; set; }
		public string Title { get; set; }
		public DateOnly? Date { get; set; }
		public TimeOnly? Time { get; set; }
		public string? AuthOrLocation { get; set; }
		public string? Genre { get; set; }
		public string Source { get; set; }
	}
}
