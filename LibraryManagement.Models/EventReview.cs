namespace LibraryManagement.Models {
    public class EventReview {
        public string UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}