using System.Collections.Generic;
using System.Reflection.Emit;
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

            builder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.BookId);
                entity.Property(b => b.Author).IsRequired();
                entity.Property(b => b.Title).IsRequired();
                entity.Property(b => b.Genre).IsRequired();
                entity.Property(b => b.Published).IsRequired();

                entity.HasMany(b => b.BookReviews)
                    .WithOne(bk => bk.Book)
                    .HasForeignKey(bk => bk.BookId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<BookReview>(entity =>
            {
                entity.HasKey(bk => bk.UserId);
                entity.HasKey(bk => bk.BookId);
                entity.Property(bk => bk.Comment).IsRequired();
                entity.Property(bk => bk.Rating).IsRequired();

                //set User relation ship
            });

            builder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.EventId);

                //set User Relationship
                entity.Property(e => e.OrganiserId);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Time).IsRequired();
                entity.Property(e => e.Location).IsRequired();
                entity.Property(e => e.Description).IsRequired();

                entity.HasMany(e => e.EventReviews)
                    .WithOne(er => er.Event)
                    .HasForeignKey(er => er.EventId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<EventReview>(entity =>
            {
                entity.HasKey(er => er.EventId);
                entity.HasKey(er => er.UserId);

                entity.Property(er => er.Rating).IsRequired();
                entity.Property(er => er.Comment).IsRequired();
            });

            builder.Entity<CheckOut>(entity =>
            {
                entity.HasKey(co => co.BookId);
                entity.HasKey(co => co.UserId);

                entity.Property(co => co.IsReturned).IsRequired();
                entity.Property(co => co.IsOverdue).IsRequired();
                entity.Property(co => co.DueDate).IsRequired();
                entity.Property(co => co.CheckoutDate).IsRequired();
                entity.Property(co => co.AuthorizeCheckout).IsRequired();
            });
        }
    }
}