using InstitueProject.Authentication_Models;
using InstitueProject.Models;
using InstitueProject.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using InstitueProject.Context;


namespace InstitueProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //***************************************************************************

            // register the built-in service 
            builder.Services.AddDbContext<ITIContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });


            // register the usermanager and signinmanager ... and registering the stores because the manager will ask for them 
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {
                    // removing some of the validation on password to make the testing easier
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireDigit = false;
                }
            )
                .AddEntityFrameworkStores<ITIContext>();

            // registering my custom services
            builder.Services.AddScoped<IDepartmentRepository , DepartmentRepository>();  // registering
            builder.Services.AddScoped<IInstructorRepository , InstructorRepository>();
            builder.Services.AddScoped<ICourseRepository , CourseRepository>();
            builder.Services.AddScoped<ITraineeRepository , TraineeRepository>();

            //****************************************************************************
            var app = builder.Build();

            #region built-in middleware

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            //*********************
            app.UseAuthentication(); // not a must because it is existed by default .. but must use in old projects    
            app.UseAuthorization();
            //*********************


            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            #endregion

            #region custom middlewares
            //app.Use(async (httpcontext, next) =>
            //{
            //    await httpcontext.Response.WriteAsync("first middleware\n");

            //    await next.Invoke();

            //    await httpcontext.Response.WriteAsync("first middleware again\n");
            //});

            //app.Use(async (httpcontext, next) =>
            //{
            //    await httpcontext.Response.WriteAsync("Second middleware\n");

            //    await next.Invoke();

            //    await httpcontext.Response.WriteAsync("Second middleware again\n");

            //});

            //app.Run(async (httpcontext) =>
            //{
            //    await httpcontext.Response.WriteAsync("Third middleware\n");
            //});
            #endregion

            app.Run();
        }
    }
}
