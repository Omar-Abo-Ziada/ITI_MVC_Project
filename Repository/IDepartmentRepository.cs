using InstitueProject.Enums;
using InstitueProject.Models;
using Microsoft.EntityFrameworkCore;

namespace InstitueProject.Repository
{
    public interface IDepartmentRepository
    {
        public List<Department> GetAll(InstructorsIncludeOptions insOption = InstructorsIncludeOptions.None,
          CourseIncludeOptions crsOption = CourseIncludeOptions.None,
          TraineeIncludeOptions traineeOption = TraineeIncludeOptions.None);

        //--------------------------------------------------------

        public List<Department> GetPageList(int skipstep, int pageSize);

        //--------------------------------------------------------

        public Department GetById(int id,
            InstructorsIncludeOptions insOption = InstructorsIncludeOptions.None,
            CourseIncludeOptions crsOption = CourseIncludeOptions.None,
            TraineeIncludeOptions traineeOption = TraineeIncludeOptions.None);

        //--------------------------------------------------------

        public void Insert(Department obj);

        //--------------------------------------------------------

        public void Update(Department obj);

        //--------------------------------------------------------

        public void Delete(int id);

        //--------------------------------------------------------

        public void Save();
      
    }
}