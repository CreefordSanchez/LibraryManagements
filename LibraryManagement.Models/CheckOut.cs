namespace LibraryManagement.Models {
	public class CheckOut {
		public int BookId { get; set; }
		public Book? Book { get; set; }
		public string UserId { get; set; }
		public bool IsReturned { get; set; }
		public bool IsOverdue { get; set; }
		public DateOnly DueDate { get; set; }
		public DateOnly CheckoutDate { get; set; }
		public bool AuthorizeCheckout { get; set; }	
	}
}