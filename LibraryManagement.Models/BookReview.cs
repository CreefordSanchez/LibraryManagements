namespace LibraryManagement.Models {
    public class BookReview {
        public string UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}