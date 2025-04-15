namespace LibraryManagement.Models.ModelViews {
	public class EventAndReview {
		public string Title { get; set; }
		public DateOnly Date { get; set; }
		public TimeOnly Time { get; set; }
		public string Location { get; set; }
		public string Description { get; set; }
		public string OrganiserID { get; set; }
		public string UserId { get; set; }
		public int Rating { get; set; }
		public string Comment { get; set; }
	}
}
