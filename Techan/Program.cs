using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Techan.DataAccessLayer;
using Techan.Models.LoginRegister;

namespace Techan
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<TechanDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(x =>
            {
                x.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyz0123456789_";
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequiredLength = 8;
                x.SignIn.RequireConfirmedEmail = false;
                x.Lockout.MaxFailedAccessAttempts = 3;
                x.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(15);
            }).AddEntityFrameworkStores<TechanDbContext>().AddDefaultTokenProviders();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
			app.MapControllerRoute(name: "login", "/Login", new
			{
				Controller = "Account",
				Action = "Login"
			});
			app.MapControllerRoute(name: "register", "/Register", new
            {
                Controller = "Account",
                Action = "Register"
            });
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
            );
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
