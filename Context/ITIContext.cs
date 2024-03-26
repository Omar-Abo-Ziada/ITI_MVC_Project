using InstitueProject.Authentication_Models;
using InstitueProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InstitueProject.Context
{
    public class ITIContext :  IdentityDbContext<ApplicationUser> // DbContext 
    {
        public DbSet<Department> Department { get; set; }

        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseResult> CourseResults { get; set; }

        public ITIContext() : base()
        {

        }

        public ITIContext(DbContextOptions<ITIContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);   Does no thing any way

          //  optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ITI_MVC;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

            // added the connection string in the app setting so that I can change it at any time even after the site is published and compiled inti IL
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region initial data as test
            // modelBuilder.Entity<Department>().HasData
            //     (
            //     new Department() { Id = 1, Name = "SD", ManagerName = "Hany"  },
            //     new Department() { Id = 2, Name = "Ui/UX", ManagerName = "Mohamed" },
            //     new Department() { Id = 3, Name = "Open Source", ManagerName = "Joshphene" },
            //     new Department() { Id = 4, Name = "BI", ManagerName = "Alaa" },
            //     new Department() { Id = 5, Name = "Devops", ManagerName = "Wael" }
            //     );

            // modelBuilder.Entity<Instructor>().HasData
            //    (
            //    new Instructor() { Id = 1, Name = "Hany", DepartmentId = 1, CourseId = 1, ImageUrl = "1.jpg" , Adress="Sohag" , Salary = 20_000},
            //    new Instructor() { Id = 2, Name = "Mohamed", DepartmentId = 2, CourseId = 1, ImageUrl = "2.jpg", Adress = "Assiut", Salary = 15_000 },
            //    new Instructor() { Id = 3, Name = "Joshphene", DepartmentId = 3, CourseId = 2, ImageUrl = "3.jpg", Adress = "Alex", Salary = 10_000 },
            //    new Instructor() { Id = 4, Name = "Alaa", DepartmentId = 4, CourseId = 2, ImageUrl = "4.jpg", Adress = "Cairo", Salary = 35_000 },
            //    new Instructor() { Id = 5, Name = "Wael", DepartmentId = 5, CourseId = 3, ImageUrl = "5.jpg", Adress = "Aswan", Salary = 25_000 }
            //    );

            // modelBuilder.Entity<Trainee>().HasData
            // (
            //     new Trainee() { Id = 1, Name = "Emad", DepartmentId = 1, ImageUrl = "6.jpg", Grade = 1 },
            //     new Trainee() { Id = 2, Name = "Hesham", DepartmentId = 2, ImageUrl = "7.jpg", Grade = 2 },
            //     new Trainee() { Id = 3, Name = "Sara", DepartmentId = 3, ImageUrl = "8.jpg", Grade = 1 },
            //     new Trainee() { Id = 4, Name = "smeer", DepartmentId = 4, ImageUrl = "9.jpg", Grade = 3 },
            //     new Trainee() { Id = 5, Name = "Amr", DepartmentId = 5, ImageUrl = "10.jpg", Grade = 4 }
            // );

            // modelBuilder.Entity<Course>().HasData
            //(
            //    new Course() { Id = 1, Name = "MVC", DepartmentId = 1, Degree = 150, MinDegree = 80 , Hours = 60 },
            //    new Course() { Id = 2, Name = "Data base", DepartmentId = 2, Degree = 100, MinDegree = 60 , Hours = 40 },
            //    new Course() { Id = 3, Name = "Cloud computing", DepartmentId = 3, Degree = 80, MinDegree = 50 , Hours = 35}
            //    //new Course() { Id = 4, Name = "IIS", DepartmentId = 4, Degree = 90 },
            //    //new Course() { Id = 5, Name = "C sharp", DepartmentId = 5, Degree = 55 }
            //);

            // modelBuilder.Entity<CourseResult>().HasData
            // (
            //     new CourseResult() { Id = 1, TraineeId = 1, CourseId = 1, Degree = 75 },
            //     new CourseResult() { Id = 2, TraineeId = 2, CourseId = 2, Degree = 80 },
            //     new CourseResult() { Id = 3, TraineeId = 3, CourseId = 3, Degree = 60 },
            //     new CourseResult() { Id = 4, TraineeId = 4, CourseId = 1, Degree = 90 },
            //     new CourseResult() { Id = 5, TraineeId = 5, CourseId = 2, Degree = 55 }
            // ); 
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
