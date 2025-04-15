using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.DAL;
using LibraryManagement.Models;
using LibraryManagement.BLL;

namespace LibraryManagements {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<LibraryManagementContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddTransient<BookRepository>();
            builder.Services.AddTransient<BookReviewRepository>();
            builder.Services.AddTransient<EventRepository>();
            builder.Services.AddTransient<EventReviewRepository>();
            builder.Services.AddTransient<CheckOutRepository>();

            builder.Services.AddTransient<BookService>();
            builder.Services.AddTransient<BookReviewService>();
            builder.Services.AddTransient<EventService>();
            builder.Services.AddTransient<EventReviewService>();
            builder.Services.AddTransient<CheckOutService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDefaultIdentity<IdentityUser>(option => option.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<LibraryManagementContext>();


            var app = builder.Build();
            SeedRoleAndAdminUserAsync(app.Services).GetAwaiter().GetResult();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
            app.Run();
        }
        static async Task SeedRoleAndAdminUserAsync(IServiceProvider serviceProvider) {
            using (IServiceScope scope = serviceProvider.CreateScope()) {
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                // Define Role
                string[] roles = { "Admin", "User" };

                foreach (string role in roles) {
                    if (!await roleManager.RoleExistsAsync(role)) {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                // Create admin user
                IdentityUser adminUser = new IdentityUser
                {
                    UserName = "AdminUser@gmail.com",
                    Email = "AdminUser@gmail.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, "Admin_1234");
                await userManager.AddToRoleAsync(adminUser, "Admin");

                for (int i = 0; i < 5; i++) {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = $"User{i}@gmail.com",
                        Email = $"User{i}@gmail.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(user, "User_123");
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }
}