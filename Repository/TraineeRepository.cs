using InstitueProject.Context;
using InstitueProject.Enums;
using InstitueProject.Models;
using InstitueProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InstitueProject.Repository
{
    public class TraineeRepository : ITraineeRepository
    {
        private readonly ITIContext context;

        public TraineeRepository(ITIContext context)
        {
            this.context = context;
        }

        //=================================================

        public List<Trainee> GetAll(DepartmentIncludeOptions depOption = DepartmentIncludeOptions.None)
        {
            IQueryable<Trainee> trainees = context.Trainees;

            if (depOption == DepartmentIncludeOptions.Include)
            {
                trainees = trainees.Include(i => i.Department);
            }

            return trainees.ToList();
        }

        public List<Trainee> GetPageList(int skipstep, int pageSize)
        {
            return context.Trainees.Skip(skipstep).Take(pageSize).Include(i => i.Department).ToList();
        }

        //--------------------------------------------------------

        public Trainee GetById(int id, DepartmentIncludeOptions depOption = DepartmentIncludeOptions.None)
        {
            IQueryable<Trainee> trainees = context.Trainees.Where(t => t.Id == id);

            if (depOption == DepartmentIncludeOptions.Include)
            {
                trainees = trainees.Include(i => i.Department);
            }

            return trainees.FirstOrDefault(i => i.Id == id);
        }

        //--------------------------------------------------------

        public void Insert(Trainee obj)
        {
            context.Add(obj);
        }
        //--------------------------------------------------------


        public void Update(Trainee obj)
        {
            context.Update(obj);
        }
        //--------------------------------------------------------


        public void Delete(int id)
        {
            Trainee trainee = GetById(id);
            context.Trainees.Remove(trainee);
        }
        //--------------------------------------------------------


        public void Save()
        {
            context.SaveChanges();
        }

        //********************************************************

        public TraineeName_CourseName_Degree_IsPassed ShowResult(int _tId, int _cId)
        {
            TraineeName_CourseName_Degree_IsPassed traineeName_CourseName_Degree_IsPassed_ViewModel
                = new TraineeName_CourseName_Degree_IsPassed();

            traineeName_CourseName_Degree_IsPassed_ViewModel.TraineeName =
                context.Trainees
                .Where(t => t.Id == _tId)
                .Select(t => t.Name).FirstOrDefault();


            traineeName_CourseName_Degree_IsPassed_ViewModel.Degree
                = context.CourseResults
               .Where(cr => cr.TraineeId == _tId && cr.CourseId == _cId)
               .Select(cr => cr.Degree).FirstOrDefault();


            var courseName_MaxDegree_MinDegree =
                context.Courses.Where(c => c.Id == _cId)
                .Select(c => new { c.Name, c.MinDegree, c.Degree }).FirstOrDefault();

            traineeName_CourseName_Degree_IsPassed_ViewModel.CourseName = courseName_MaxDegree_MinDegree.Name;

            traineeName_CourseName_Degree_IsPassed_ViewModel.MaxDegree = courseName_MaxDegree_MinDegree.Degree;

            if (traineeName_CourseName_Degree_IsPassed_ViewModel.Degree >= courseName_MaxDegree_MinDegree.MinDegree)
            {
                traineeName_CourseName_Degree_IsPassed_ViewModel.IsPassed = true;
            }
            else
            {
                traineeName_CourseName_Degree_IsPassed_ViewModel.IsPassed = false;
            }

            return traineeName_CourseName_Degree_IsPassed_ViewModel;
        }

        public List<TraineeName_CourseName_Degree_IsPassed> ShowTraineeResult(int _tId)
        {
            List<TraineeName_CourseName_Degree_IsPassed> viewModels =
                new List<TraineeName_CourseName_Degree_IsPassed>();

            List<CourseResult> courseResults = context.CourseResults
                 .Where(cr => cr.TraineeId == _tId )
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




    }
}
