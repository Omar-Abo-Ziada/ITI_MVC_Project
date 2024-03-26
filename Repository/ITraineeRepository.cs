using InstitueProject.Enums;
using InstitueProject.Models;
using InstitueProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InstitueProject.Repository
{
    public interface ITraineeRepository
    {
        public List<Trainee> GetAll(DepartmentIncludeOptions depOption = DepartmentIncludeOptions.None);

        public List<Trainee> GetPageList(int skipstep, int pageSize);

        //--------------------------------------------------------

        public Trainee GetById(int id, DepartmentIncludeOptions depOption = DepartmentIncludeOptions.None);

        //--------------------------------------------------------

        public void Insert(Trainee obj);

        //--------------------------------------------------------

        public void Update(Trainee obj);

        //--------------------------------------------------------

        public void Delete(int id);

        //--------------------------------------------------------

        public void Save();

        //--------------------------------------------------------

        public TraineeName_CourseName_Degree_IsPassed ShowResult(int _tId, int _cId);


        public List<TraineeName_CourseName_Degree_IsPassed> ShowTraineeResult(int _tId);

    }
}
