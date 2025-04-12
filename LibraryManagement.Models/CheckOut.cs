namespace LibraryManagement.Models {
    public class CheckOut {
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public string UserId { get; set; }
        // add User reference here 
        public bool IsReturned { get; set; }
        public bool IsOverdue { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public bool AuthorizeCheckout { get; set; }
    }
}