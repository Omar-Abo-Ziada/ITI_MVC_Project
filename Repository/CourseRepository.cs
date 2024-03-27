using InstitueProject.Enums;
using InstitueProject.Models;
using InstitueProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InstitueProject.Context;


namespace InstitueProject.Repository
{

    public class CourseRepository : ICourseRepository 
    {
        ITIContext context;

        public string id { get; set; }

        public CourseRepository(ITIContext _context)
        {
            context = _context;

            id = Guid.NewGuid().ToString();
        }

        //********************************************************

        public List<Course> GetAll
            (DepartmentIncludeOptions departmentOption = DepartmentIncludeOptions.None,
            InstructorsIncludeOptions instructorsOption = InstructorsIncludeOptions.None)
        {
            IQueryable<Course> courses = context.Courses;

            if (departmentOption == DepartmentIncludeOptions.None
                && instructorsOption == InstructorsIncludeOptions.None)
            {
                // do no thing because at the wnd i will return courses.ToList()
            }

            if (departmentOption == DepartmentIncludeOptions.Include)
            {
                courses = courses.Include(c => c.Department);
            }

            if (instructorsOption == InstructorsIncludeOptions.Include)
            {
                courses = courses.Include(c => c.Instructors);
            }

            return courses.ToList();
        }

        public List<Course> GetPageList(int skipstep, int pageSize)
        {
            return context.Courses.Skip(skipstep).Take(pageSize).Include(c => c.Department).ToList();
        }

        //--------------------------------------------------------

        public Course GetById(int id,
          DepartmentIncludeOptions departmentOption = DepartmentIncludeOptions.None,
          InstructorsIncludeOptions instructorsOption = InstructorsIncludeOptions.None)
        {
            /// TODO : test it on one controller and if success => apply it on the rest 
            /// DONE : hhhhaha It Worked ... and Applied on the Rest :D
           
            IQueryable<Course> courses = context.Courses.Where(c => c.Id == id);  // I think this is better way because it doenst get all courses and include with them dep and ins but inly one crs .Where(c => c.Id == id);

            if (departmentOption == DepartmentIncludeOptions.Include)
            {
                courses = courses.Include(c => c.Department);
            }

            if (instructorsOption == InstructorsIncludeOptions.Include)
            {
                courses = courses.Include(c => c.Instructors).ThenInclude(i => i.Department);
            }

            return courses.FirstOrDefault();
        }

        #region question about IQurable
        //===========================================
        // why i can't fo it like this .. i want to get the specific crs i need only then include what ever i need ?

        //public Course GetById(int id,
        //DepartmentIncludeOptions departmentOption = DepartmentIncludeOptions.None,
        //InstructorsIncludeOptions instructorsOption = InstructorsIncludeOptions.None)
        //{

        //    IQueryable crs = context.Courses.FirstOrDefault(c => c.Id == id) as IQueryable;

        //    if (Department == null && Instructors == null)
        //    {

        //    }

        //    if (Department != null)
        //    {
        //        if (Department.ToLower() == "department")
        //        {
        //            crs.Include(c => c.Department).FirstOrDefault(c => c.Id == id);
        //        }
        //    }


        //    if (Instructors != null)
        //    {
        //        if (Instructors.ToLower() == "instructors")
        //        {
        //            crs.Include(c => c.Instructors).FirstOrDefault(c => c.Id == id);
        //        }
        //    }

        //    return crs as Course;
        //}
        //=========================================== 
        #endregion

        //--------------------------------------------------------

        public List<TraineeName_CourseName_Degree_IsPassed> ShowCourseResults(Course crs)
        {
            List<TraineeName_CourseName_Degree_IsPassed> viewModels =
                new List<TraineeName_CourseName_Degree_IsPassed>();

            List<CourseResult> courseResults = context.CourseResults
                 .Where(cr => cr.CourseId == crs.Id)
                 .Include(cr => cr.Trainee)
                 .Include(cr => cr.Course).ToList();

            foreach (CourseResult cr in courseResults)
            {
                TraineeName_CourseName_Degree_IsPassed viewModel =
                    new TraineeName_CourseName_Degree_IsPassed();

                viewModel.TraineeName = cr.Trainee.Name;
                viewModel.CourseName = cr.Course.Name;
                viewModel.Degree = cr.Degree;
                viewModel.MaxDegree = cr.Course.Degree;
                viewModel.IsPassed = cr.Degree >= cr.Course.MinDegree;

                viewModels.Add(viewModel);
            }

            return viewModels;
        }


        //--------------------------------------------------------

        public void Insert(Course obj)
        {
            context.Add(obj);
        }

        //--------------------------------------------------------


        public void Update(Course obj)
        {
            context.Update(obj);
        }
        //--------------------------------------------------------


        public void Delete(int id)
        {
            Course crs = GetById(id);
            context.Courses.Remove(crs);
        }
        //--------------------------------------------------------


        public void Save()
        {
            context.SaveChanges();
        }

        //********************************************************

    }
}