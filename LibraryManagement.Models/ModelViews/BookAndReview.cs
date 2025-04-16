namespace LibraryManagement.Models.ModelViews {
	public class BookAndReview {
		public string Title { get; set; }
		public string Author { get; set; }
		public string Genre { get; set; }
		public DateOnly Published { get; set; }
		public string UserId { get; set; }
		public int Rating { get; set; }
		public string Comment { get; set; }

	}
}
