using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DAL {
	public class LibraryManagementContext : IdentityDbContext<IdentityUser> {
		public LibraryManagementContext(DbContextOptions<LibraryManagementContext> options) : base(options) { }

		public DbSet<Book> Books { set; get; }
		public DbSet<BookReview> BookReviews { set; get; }
		public DbSet<Event> Events { set; get; }
		public DbSet<EventReview> EventReviews { set; get; }
		public DbSet<CheckOut> CheckOuts { set; get; }

		protected override void OnModelCreating(ModelBuilder builder) {
			base.OnModelCreating(builder);

			builder.Entity<Book>(entity => {
				entity.HasKey(b => b.BookId);
				entity.Property(b => b.Author).IsRequired();
				entity.Property(b => b.Title).IsRequired();
				entity.Property(b => b.Genre).IsRequired();
				entity.Property(b => b.Published).IsRequired();
				entity.Property(b => b.Picture).IsRequired();

				entity.HasMany(b => b.BookReviews)
					.WithOne(bk => bk.Book)
					.HasForeignKey(bk => bk.BookId)
					.OnDelete(DeleteBehavior.Cascade);
			});

			builder.Entity<BookReview>(entity => {
				entity.HasKey(bk => bk.BookReviewId);

				entity.Property(bk => bk.Comment).IsRequired();
				entity.Property(bk => bk.Rating).IsRequired();

				entity.HasOne<IdentityUser>()
					.WithMany()
					.HasForeignKey(bk => bk.UserId)
					.OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(bk => bk.Book)
					.WithMany(b => b.BookReviews)
					.HasForeignKey(bk => bk.BookId)
					.OnDelete(DeleteBehavior.Cascade);
			});

			builder.Entity<Event>(entity => {
				entity.HasKey(e => e.EventId);

				entity.Property(e => e.Title).IsRequired();
				entity.Property(e => e.Date).IsRequired();
				entity.Property(e => e.Time).IsRequired();
				entity.Property(e => e.Location).IsRequired();
				entity.Property(e => e.Description).IsRequired();

				entity.HasOne<IdentityUser>()
					.WithMany()
					.HasForeignKey(e => e.OrganiserId)
					.OnDelete(DeleteBehavior.Cascade);
			});

			builder.Entity<EventReview>(entity => {
				entity.HasKey(er => er.EventReviewId);

				entity.Property(er => er.Rating).IsRequired();
				entity.Property(er => er.Comment).IsRequired();

				entity.HasOne<IdentityUser>()
				   .WithMany()
				   .HasForeignKey(er => er.UserId)
				   .OnDelete(DeleteBehavior.NoAction);

				entity.HasOne(er => er.Event)
					.WithMany(e => e.EventReviews)
					.HasForeignKey(er => er.EventId)
					.OnDelete(DeleteBehavior.Cascade);
			});

			builder.Entity<CheckOut>(entity => {
				entity.HasKey(co => co.CheckOutId);

				entity.Property(co => co.IsReturned).IsRequired();
				entity.Property(co => co.IsOverdue).IsRequired();
				entity.Property(co => co.DueDate).IsRequired();
				entity.Property(co => co.CheckoutDate).IsRequired();
				entity.Property(co => co.AuthorizeCheckout).IsRequired();

				entity.HasOne<IdentityUser>()
					.WithMany()
					.HasForeignKey(co => co.UserId)
					.OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(co => co.Book)
					.WithMany(b => b.CheckOut)
					.HasForeignKey(co => co.BookId)
					.OnDelete(DeleteBehavior.Cascade);
			});

            //NOTE: run the program first and go to SQL server to grab Users id and put it here
            
            
            //builder.Entity<Book>().HasData(
            //    new Book
            //    {
            //        BookId = 1,
            //        Title = "Airborn",
            //        Author = "Kenneth Oppel",
            //        Genre = "Adventure",
            //        Published = new DateOnly(2004, 5, 1),
            //        Picture = "~/img/AirbornBook.jpg"
            //    },
            //    new Book
            //    {
            //        BookId = 2,
            //        Title = "Harry Potter",
            //        Author = "J.K. Rowling",
            //        Genre = "Fantasy",
            //        Published = new DateOnly(1997, 6, 26),
            //        Picture = "~/img/HarryPotterBook.jpg"
            //    },
            //    new Book
            //    {
            //        BookId = 3,
            //        Title = "The Hunger Games",
            //        Author = "Suzanne Collins",
            //        Genre = "Dystopian",
            //        Published = new DateOnly(2008, 9, 14),
            //        Picture = "~/img/HungerGameBook.webp"
            //    },
            //    new Book
            //    {
            //        BookId = 4,
            //        Title = "Percy Jackson",
            //        Author = "Rick Riordan",
            //        Genre = "Fantasy",
            //        Published = new DateOnly(2005, 6, 28),
            //        Picture = "~/img/PercyJacksonBook.jpg"
            //    },
            //    new Book
            //    {
            //        BookId = 5,
            //        Title = "Twilight",
            //        Author = "Stephenie Meyer",
            //        Genre = "Romance",
            //        Published = new DateOnly(2005, 10, 5),
            //        Picture = "~/img/TwilightBook.jpg"
            //    },
            //    new Book
            //    {
            //        BookId = 7,
            //        Title = "Wonder",
            //        Author = "R.J. Palacio",
            //        Genre = "Drama",
            //        Published = new DateOnly(2012, 2, 14),
            //        Picture = "~/img/WonderBook.jpg"
            //    }
            //);

            //builder.Entity<BookReview>().HasData(
            //    new BookReview
            //    {
            //        BookReviewId = 1,
            //        UserId = " ",
            //        Rating = 5,
            //        Comment = "An absolutely amazing read! Couldn't put it down.",
            //        BookId = 2 
            //    },
            //    new BookReview
            //    {
            //        BookReviewId = 2,
            //        UserId = " ",
            //        Rating = 4,
            //        Comment = "Great story and characters. Really enjoyed it.",
            //        BookId = 3 
            //    }
            //);

            //builder.Entity<CheckOut>().HasData(
            //    // 1. Not Returned, Overdue (30 days ago)
            //    new CheckOut
            //    {
            //        CheckOutId = 1,
            //        BookId = 1,
            //        UserId = " ",
            //        IsReturned = false,
            //        IsOverdue = true,
            //        DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-30)), // 30 days ago
            //        CheckoutDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-35)), // 5 days before due
            //        AuthorizeCheckout = true
            //    },
            //    // 2. Not Returned, Still On Time (Due in 5 days)
            //    new CheckOut
            //    {
            //        CheckOutId = 2,
            //        BookId = 2,
            //        UserId = " ",
            //        IsReturned = false,
            //        IsOverdue = false,
            //        DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5)), // 5 days from now
            //        CheckoutDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-25)), // 25 days ago
            //        AuthorizeCheckout = true
            //    },
            //    // 3. Returned (Returned 10 days ago)
            //    new CheckOut
            //    {
            //        CheckOutId = 3,
            //        BookId = 3,
            //        UserId = " ",
            //        IsReturned = true,
            //        IsOverdue = false, 
            //        DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30)), 
            //        CheckoutDate = DateOnly.FromDateTime(DateTime.Now), 
            //        AuthorizeCheckout = true
            //    }
            //);

            //builder.Entity<Event>().HasData(
            //    new Event
            //    {
            //        EventId = 1,
            //        Title = "Book Launch: Airborn",
            //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(10)), // Event happening 10 days from today
            //        Time = TimeOnly.FromDateTime(DateTime.Now.AddHours(14)), // 2:00 PM
            //        Location = "Library Hall A",
            //        Description = "Join us for the launch of the book 'Airborn' by Kenneth Oppel. Meet the author and other fans!",
            //        OrganiserId = " "
            //    },
            //    new Event
            //    {
            //        EventId = 2,
            //        Title = "Harry Potter Trivia Night",
            //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(20)), // Event happening 20 days from today
            //        Time = TimeOnly.FromDateTime(DateTime.Now.AddHours(18)), // 6:00 PM
            //        Location = "Library Hall B",
            //        Description = "Test your knowledge of the Harry Potter universe at our trivia night. Great prizes for the winners!",
            //        OrganiserId = " "
            //    },
            //    new Event
            //    {
            //        EventId = 3,
            //        Title = "Twilight Screening & Discussion",
            //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(30)), 
            //        Time = TimeOnly.FromDateTime(DateTime.Now.AddHours(19)), 
            //        Location = "Library Hall C",
            //        Description = "Come watch 'Twilight' with fellow fans and participate in a discussion afterward.",
            //        OrganiserId = " "
            //    },
            //    new Event
            //    {
            //        EventId = 4,
            //        Title = "Percy Jackson Reading & Meet",
            //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(50)), // Event happening 50 days from today
            //        Time = TimeOnly.FromDateTime(DateTime.Now.AddHours(11)), // 11:00 AM
            //        Location = "Library Hall D",
            //        Description = "Join us for a reading of 'Percy Jackson' and meet other fans of the series.",
            //        OrganiserId = " "
            //    }
            //);          

            builder.Entity<EventReview>().HasData(
                new EventReview
                {
                    EventReviewId = 1,
                    UserId = " ",
                    Rating = 5, 
                    Comment = "Fantastic event! Great organization and the speakers were excellent.",
                    EventId = 1 
                },
                new EventReview
                {
                    EventReviewId = 2,
                    UserId = " ",
                    Rating = 4,
                    Comment = "Good event, but the schedule could've been a bit tighter. Still enjoyable.",
                    EventId = 2 
                }
            );
            
        }
    }
}