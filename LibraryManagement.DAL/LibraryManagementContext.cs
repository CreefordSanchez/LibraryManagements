using System.Collections.Generic;
using System.Reflection.Emit;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DAL
{
    public class LibraryManagementContext : IdentityDbContext<IdentityUser>
    {
        public LibraryManagementContext(DbContextOptions<LibraryManagementContext> options) : base(options) { }

        public DbSet<Book> Books { set; get; }
        public DbSet<BookReview> BookReviews { set; get; }
        public DbSet<Event> Events { set; get; }
        public DbSet<EventReview> EventReviews { set; get; }
        public DbSet<CheckOut> CheckOuts { set; get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>(entity =>
            {
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

            builder.Entity<BookReview>(entity =>
            {
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

            builder.Entity<Event>(entity =>
            {
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

            builder.Entity<EventReview>(entity =>
            {
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

            builder.Entity<CheckOut>(entity =>
            {
                entity.HasKey(co => co.BookId);

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
                    .WithOne(b => b.CheckOut)
                    .HasForeignKey<CheckOut>(co => co.BookId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}