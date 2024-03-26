using InstitueProject.Enums;
using InstitueProject.Models;
using InstitueProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InstitueProject.Repository
{
    public interface ICourseRepository
    {
         string id { get; set; }
        //--------------------------------------------------------

        public List<Course> GetAll
          (DepartmentIncludeOptions departmentOption = DepartmentIncludeOptions.None,
          InstructorsIncludeOptions instructorsOption = InstructorsIncludeOptions.None);

        //--------------------------------------------------------

        public Course GetById(int id,
          DepartmentIncludeOptions departmentOption = DepartmentIncludeOptions.None,
          InstructorsIncludeOptions instructorsOption = InstructorsIncludeOptions.None);

        //--------------------------------------------------------

        public List<Course> GetPageList(int skipstep, int pageSize);

        //--------------------------------------------------------

        public List<TraineeName_CourseName_Degree_IsPassed> ShowCourseResults(Course crs);

        //--------------------------------------------------------


        public void Insert(Course obj);

        //--------------------------------------------------------


        public void Update(Course obj);

        //--------------------------------------------------------


        public void Delete(int id);

        //--------------------------------------------------------


        public void Save();

    }
}