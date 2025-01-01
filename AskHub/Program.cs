using AskHub.Controllers;
using AskHub.Models;
using AskHub.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace AskHub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // -------------------------------------------------------
            builder.Services.AddDbContext<AppDbContext>(
                optionsBuilder =>
                {
                    optionsBuilder.UseSqlServer("Data Source=DESKTOP-40BVR0R\\SQLEXPRESS;Initial Catalog=AskHubDatabase;Integrated Security=True");
                });

            builder.Services.AddScoped<IQuestionRepository, QuestionSqlRepository>();

            builder.Services.AddScoped<IAnswerRepository, AnswerSqlRepository>();

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(14);
                options.SlidingExpiration = true;
            });

            builder.Services.AddAuthorization();
            //-------------------------------------------------------

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
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}