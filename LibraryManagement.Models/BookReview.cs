using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models {
    public class BookReview {
        public int BookReviewId { get; set; }
        public string UserId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}