
using Business.Services.Abstract;
using Business.Services.Concretes;
using Core.Models;
using Core.RepoAbstract;
using Data.DAL;
using Data.RepoConcretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ProniaTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(opt=>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
            );


            //builder.Services.AddIdentity<AppUser, IdentityUser>(opt =>
            //{
            //    opt.Password.RequiredLength = 8;
            //    opt.Password.RequireNonAlphanumeric = true;
            //    opt.Password.RequireDigit = true;
            //    opt.Password.RequireLowercase = true;
            //    opt.Password.RequireUppercase = true;

            //    opt.User.RequireUniqueEmail = false;
            //}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();



            builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            builder.Services.AddScoped<IPortfolioService, PortfolioService>();

            // Add services to the container.

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

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