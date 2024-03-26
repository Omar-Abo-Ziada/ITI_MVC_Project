using InstitueProject.Context;
using InstitueProject.Enums;
using InstitueProject.Models;
using InstitueProject.Repository;
using InstitueProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol.Core.Types;
using System.Configuration;

namespace InstitueProject.Controllers
{
    [Authorize]
    public class TraineeController : Controller
    {
        private readonly ITraineeRepository traineeRepository;
        private readonly IDepartmentRepository departmentRepository;
        //  private readonly IInstructorRepository instructorRepository;

        public TraineeController
            (ITraineeRepository traineeRepository,
            IDepartmentRepository departmentRepository,
            IInstructorRepository instructorRepository)
        {
            this.traineeRepository = traineeRepository;
            this.departmentRepository = departmentRepository;
            //   this.instructorRepository = instructorRepository;
        }

        //==============================================================

        public IActionResult All(int page = 1, int pageSize = 3)
        {
            int skipCount = (page - 1) * pageSize;

            List<Trainee> paginatedTrainees = traineeRepository.GetPageList(skipCount, pageSize);

            int totalCount = traineeRepository.GetAll().Count();

            ViewData["TotalPages"] = Math.Ceiling(totalCount / (double)pageSize); // Correcting the division to avoid integer division

            ViewData["AllTraineesNames"] = traineeRepository.GetAll().Select(i => i.Name).ToList();

            return View(paginatedTrainees);
        }

        public IActionResult AllPartial(int page = 1, int pageSize = 3)
        {
            int skipCount = (page - 1) * pageSize;

            List<Trainee> paginatedTrainees = traineeRepository.GetPageList(skipCount, pageSize);

            int totalCount = traineeRepository.GetAll().Count();

            ViewData["TotalPages"] = Math.Ceiling(totalCount / (double)pageSize); // Correcting the division to avoid integer division

            List<string> allTraineesNames = traineeRepository.GetAll().Select(i => i.Name).ToList();
            ViewData["AllTraineesNames"] = allTraineesNames; // Pass the list directly to the ViewData

            return PartialView("_TraineeTablePartial", paginatedTrainees);
        }

        //------------------------------------

        public IActionResult Search(string searchTraineeName)
        {
            if (searchTraineeName != null)
            {
                List<Trainee> filteredTrainess = traineeRepository.GetAll()
                .Where(t => t.Name.ToLower().Contains(searchTraineeName.ToLower())).ToList();

                List<string> allTraineesNames = traineeRepository.GetAll().Select(i => i.Name).ToList();

                ViewData["AllTraineesNames"] = allTraineesNames; // Pass the list directly to the ViewData

                return View("All", filteredTrainess);
            }
            return RedirectToAction("All");
        }

        //------------------------------------

        public IActionResult Details(int id)
        {
            Trainee trainee = traineeRepository.GetById(id, DepartmentIncludeOptions.Include);

            return View(trainee);
        }

        //-----------------------------------

        [Authorize(Roles = "Admin")]
        public IActionResult New()
        {
            TraineeViewModel traineeVM = new TraineeViewModel();

            traineeVM.Departments = departmentRepository.GetAll();

            return View(traineeVM);
        }

        [HttpPost]
        public IActionResult SaveNew(TraineeViewModel traineeVM)
        {
            if (ModelState.IsValid)
            {
                Trainee newtrainee = new Trainee();

                newtrainee.Id = traineeVM.Id;
                newtrainee.Name = traineeVM.Name;
                newtrainee.Adress = traineeVM.Adress;
                newtrainee.ImageUrl = traineeVM.ImageUrl;
                newtrainee.Grade = traineeVM.Grade;

                newtrainee.DepartmentId = traineeVM.DepartmentId;
                newtrainee.Department = departmentRepository.GetById(traineeVM.DepartmentId);

                traineeRepository.Insert(newtrainee);

                traineeRepository.Save();
                departmentRepository.Save();

                return RedirectToAction("All");
            }
            else
            {
                return View("New", traineeVM);
            }
        }

        //-----------------------------------

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(TraineeViewModel traineeVM)
        {
            Trainee trainee = traineeRepository.GetById(traineeVM.Id);

            traineeVM.Name = trainee.Name;
            traineeVM.Adress = trainee.Adress;
            traineeVM.ImageUrl = trainee.ImageUrl;
            //traineeVM.Grade = trainee.Grade != null ? traineeVM.Grade : 0 ;
            traineeVM.Grade = trainee.Grade;

            traineeVM.DepartmentId = trainee.DepartmentId;
            traineeVM.Department = trainee.Department;

            traineeVM.Departments = departmentRepository.GetAll();

            return View(traineeVM);
        }


        [HttpPost]
        public IActionResult SaveEdited(TraineeViewModel traineeVM)
        {
            if (ModelState.IsValid)
            {
                Trainee trainee = traineeRepository.GetById(traineeVM.Id);

                trainee.Name = traineeVM.Name;
                trainee.Adress = traineeVM.Adress;
                trainee.ImageUrl = traineeVM.ImageUrl;
                //traineeVM.Grade = trainee.Grade != null ? traineeVM.Grade : 0;
                trainee.Grade = traineeVM.Grade;

                trainee.DepartmentId = traineeVM.DepartmentId;
                trainee.Department = departmentRepository.GetById(traineeVM.DepartmentId);

                traineeRepository.Save();
                departmentRepository.Save();

                return RedirectToAction("All");
            }
            else
            {
                return View("Edit", traineeVM);
            }
        }

        //--------------------------------------------------

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            ViewData["id"] = id;
            return View("Delete");
        }

        public IActionResult SaveDelete(int id)
        {
            Trainee deletedTraine = traineeRepository.GetById(id);

            if (deletedTraine != null)
            {
                traineeRepository.Delete(deletedTraine.Id);

                traineeRepository.Save();
                departmentRepository.Save(); // is it necessary ?

                return RedirectToAction("All");
            }
            else
            {
                return RedirectToAction("All");
            }
        }

        //=============================================================
        public IActionResult ShowResult(int _tId, int _cId)
        {
            TraineeName_CourseName_Degree_IsPassed traineeName_CourseName_Degree_IsPassed_ViewModel
                = traineeRepository.ShowResult(_tId, _cId);

            return View(traineeName_CourseName_Degree_IsPassed_ViewModel);
        }

        public IActionResult ShowTraineeResult(Trainee trainee)
        {
            List<TraineeName_CourseName_Degree_IsPassed> viewModels =
                traineeRepository.ShowTraineeResult(trainee.Id);

            return View(viewModels);

        }

        //[Authorize(Roles = "Admin")]
        //public IActionResult ShowCourseResults(Course crs)
        //{
        //    List<TraineeName_CourseName_Degree_IsPassed> viewModels =
        //        traineeRepository.ShowCourseResults(crs.Id);

        //    return View(viewModels);
        //}
    }
}
