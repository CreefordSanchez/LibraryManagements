namespace LibraryManagement.Models {
    public class Event {
        public int EventId { get; set; }
        public string Title { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string OrganiserId { get; set; }
        public ICollection<EventReview>? EventReviews { get; set; }
    }
}