using InstitueProject.Enums;
using InstitueProject.Models;
using Microsoft.EntityFrameworkCore;

namespace InstitueProject.Repository
{
    public interface IInstructorRepository
    {
        public List<Instructor> GetAll(DepartmentIncludeOptions depOption = DepartmentIncludeOptions.None
                                  , CourseIncludeOptions crsOption = CourseIncludeOptions.None);

        //--------------------------------------------------------

        public List<Instructor> GetPageList(int skipstep, int pageSize);


        //--------------------------------------------------------

        public Instructor GetById(int id, DepartmentIncludeOptions depOption = DepartmentIncludeOptions.None
                                    , CourseIncludeOptions crsOption = CourseIncludeOptions.None);


        //--------------------------------------------------------

        public void Insert(Instructor obj);

        //--------------------------------------------------------


        public void Update(Instructor obj);

        //--------------------------------------------------------


        public void Delete(int id);

        //--------------------------------------------------------

        public void Save();
  
    }
}