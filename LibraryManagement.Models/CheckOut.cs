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

		// These may be able to be removed and we can alter DueDate and CheckoutDate to be DateOnly
		public DateOnly GetDueDate() {
			return DateOnly.FromDateTime(this.DueDate);
		}

		public DateOnly GetCheckoutDate() {
			return DateOnly.FromDateTime(this.CheckoutDate);


		public static DateOnly GetToday() {
			return DateOnly.FromDateTime(DateTime.Now);
		}
	}
}