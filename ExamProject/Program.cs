using ExamProject.DAL.Context;
using ExamProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExamProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            builder.Services.AddIdentity<Appuser, IdentityRole>(opt =>
            {
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-";
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequiredUniqueChars = 0;
                
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.AddAuthentication().AddCookie(opt =>
            {
                opt.LoginPath = @"/Account/Login";
                opt.Cookie.Expiration = TimeSpan.FromDays(20);
                opt.AccessDeniedPath = @"/Account/AccessDeniedPath";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
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
