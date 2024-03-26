using InstitueProject.Enums;
using InstitueProject.Models;
using Microsoft.EntityFrameworkCore;
using InstitueProject.Context;


namespace InstitueProject.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        ITIContext context;

        public DepartmentRepository(ITIContext _context)
        {
            context = _context;
        }

        //************************************

        public List<Department> GetAll(InstructorsIncludeOptions insOption = InstructorsIncludeOptions.None,
            CourseIncludeOptions crsOption = CourseIncludeOptions.None,
            TraineeIncludeOptions traineeOption = TraineeIncludeOptions.None)
        {
            IQueryable<Department> departments = context.Department;

            if (insOption == InstructorsIncludeOptions.None
                && crsOption == CourseIncludeOptions.None
                && traineeOption == TraineeIncludeOptions.None)
            {
                // do no thing and leave it to the return statment
            }

            if (insOption == InstructorsIncludeOptions.Include)
            {
                departments=  departments.Include(d => d.Instructors);
            }

            if (crsOption == CourseIncludeOptions.Include)
            {
                departments =departments.Include(d => d.Courses);
            }

            if (traineeOption == TraineeIncludeOptions.Include)
            {
                departments = departments.Include(d => d.Trainees);
            }

            return departments.ToList();

        }

        public List<Department> GetPageList(int skipstep, int pageSize)
        {
            return context.Department.Skip(skipstep).Take(pageSize).ToList();
        }

        //--------------------------------------------------------

        public Department GetById(int id,
            InstructorsIncludeOptions insOption = InstructorsIncludeOptions.None,
            CourseIncludeOptions crsOption = CourseIncludeOptions.None,
            TraineeIncludeOptions traineeOption = TraineeIncludeOptions.None)
        {
            IQueryable<Department> departments = context.Department;

            if (insOption == InstructorsIncludeOptions.None
                && crsOption == CourseIncludeOptions.None
                && traineeOption == TraineeIncludeOptions.None)
            {
                // do no thing and leave it to the return statment
            }

            if (insOption == InstructorsIncludeOptions.Include)
            {
                departments=  departments.Include(d => d.Instructors);
            }

            if (crsOption == CourseIncludeOptions.Include)
            {
                departments = departments.Include(d => d.Courses);
            }

            if (traineeOption == TraineeIncludeOptions.Include)
            {
                departments = departments.Include(d => d.Trainees);
            }

            return departments.FirstOrDefault(d => d.Id == id);

        }

        //--------------------------------------------------------

        public void Insert(Department obj)
        {
            context.Add(obj);
        }
        //--------------------------------------------------------


        public void Update(Department obj)
        {
            context.Update(obj);
        }
        //--------------------------------------------------------


        public void Delete(int id)
        {
            Department dep = GetById(id);
            context.Department.Remove(dep);
        }
        //--------------------------------------------------------


        public void Save()
        {
            context.SaveChanges();
        }

        //********************************************************
    }
}
