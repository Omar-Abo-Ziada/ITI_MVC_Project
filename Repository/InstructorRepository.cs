using InstitueProject.Context;
using InstitueProject.Enums;
using InstitueProject.Models;
using Microsoft.EntityFrameworkCore;

namespace InstitueProject.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        ITIContext context;

        public InstructorRepository(ITIContext _context)
        {
            context = _context;
        }

        //********************************

        public List<Instructor> GetAll(DepartmentIncludeOptions depOption = DepartmentIncludeOptions.None
                                    , CourseIncludeOptions crsOption = CourseIncludeOptions.None)
        {
            IQueryable<Instructor> instructors = context.Instructors;

            if (depOption == DepartmentIncludeOptions.Include)
            {
                instructors =instructors.Include(i => i.Department);
            }

            if (crsOption == CourseIncludeOptions.Include)
            {
                instructors = instructors.Include(i => i.Course);

            }

            return instructors.ToList();
        }

        public List<Instructor> GetPageList(int skipstep, int pageSize)
        {
            return context.Instructors.Skip(skipstep).Take(pageSize).Include(i => i.Course).Include(i => i.Department).ToList();
        }

        //--------------------------------------------------------

        public Instructor GetById(int id, DepartmentIncludeOptions depOption = DepartmentIncludeOptions.None
                                    , CourseIncludeOptions crsOption = CourseIncludeOptions.None)
        {
            IQueryable<Instructor> instructors = context.Instructors;

            if (depOption == DepartmentIncludeOptions.Include)
            {
                instructors= instructors.Include(i => i.Department);
            }

            if (crsOption == CourseIncludeOptions.Include)
            {
                instructors = instructors.Include(i => i.Course);

            }

            return instructors.FirstOrDefault(i => i.Id == id);
        }

        //--------------------------------------------------------

        public void Insert(Instructor obj)
        {
            context.Add(obj);
        }
        //--------------------------------------------------------


        public void Update(Instructor obj)
        {
            context.Update(obj);
        }
        //--------------------------------------------------------


        public void Delete(int id)
        {
            Instructor ins = GetById(id);
            context.Instructors.Remove(ins);
        }
        //--------------------------------------------------------


        public void Save()
        {
            context.SaveChanges();
        }

        //********************************************************
    }
}
