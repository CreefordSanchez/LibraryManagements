using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models {
    public class EventReview {
        public int EventReviewId { get; set; }
        public string UserId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }
    }
}