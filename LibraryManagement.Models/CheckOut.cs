namespace LibraryManagement.Models {
    public class CheckOut {
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public string UserId { get; set; }
        public bool IsReturned { get; set; }
        public bool IsOverdue { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public bool AuthorizeCheckout { get; set; }

        public DateOnly GetDueDate() {
            return DateOnly.FromDateTime(this.DueDate);
		}

        public static DateOnly GetToday() {
			return DateOnly.FromDateTime(DateTime.Now);
		}
	}
}