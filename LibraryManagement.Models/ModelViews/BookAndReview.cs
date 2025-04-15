using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models.ModelViews
{
    public class BookAndReview
    {
        public string Title { get; set; }
        public string Author { get; set; }
		public string Genre { get; set; }
		public DateTime Published { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
		public string Comment { get; set; }

	}
}
