using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models.ModelViews
{
    public class BookAndEvent
    {
		public int BookId { get; set; }
		public string BookTitle { get; set; }
		public string BookAuthor { get; set; }
        public DateOnly BookPublished { get; set; }
		public string BookGenre { get; set; }

		public int EventId { get; set; }
		public string EventTitle { get; set; }
        public DateOnly EventDate { get; set; }
        public TimeOnly EventTime { get; set; }
		public string EventLocation { get; set; }
	}
}
