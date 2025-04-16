using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models {
    public class Book {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateOnly Published { get; set; }
        public ICollection<BookReview>? BookReviews { get; set; }
        public ICollection<CheckOut>? CheckOut { get; set; }
        public string Picture { get; set; }
    }
}