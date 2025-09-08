using Graduation_Project.BLL.Services;
using Graduation_Project.BLL.Services.Interfaces;
using Graduation_Project.DAl.Data;
using Graduation_Project.DAl.Repositories;
using Graduation_Project.DAl.Repositories.CourseRepo;
using Graduation_Project.DAl.Repositories.GradeRepo;
using Graduation_Project.DAl.Repositories.SessionRepo;
using Graduation_Project.DAl.Repositories.UserRepo;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddScoped<IGradeRepository, GradeRepository>();
            


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IGradeService, GradeService>();
            ;




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
