using InstitueProject.Enums;
using InstitueProject.Models;
using InstitueProject.Repository;
using InstitueProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InstitueProject.Controllers
{
    [Authorize]
    public class InstructorController : Controller
    {
        IInstructorRepository instructorRepository;
        IDepartmentRepository departmentRepository;
        ICourseRepository courseRepository;

        public InstructorController( IInstructorRepository insRepo , IDepartmentRepository depRepo , ICourseRepository crsRepo)
        {
            instructorRepository = insRepo;
            departmentRepository = depRepo;
            courseRepository = crsRepo;
        }

        public IActionResult All(int page = 1, int pageSize = 3)
        {
            int skipCount = (page - 1) * pageSize;

            List<Instructor> paginatedInstructors = instructorRepository.GetPageList(skipCount, pageSize);


            int totalCount = instructorRepository.GetAll().Count();

            ViewData["TotalPages"] = Math.Ceiling(totalCount / (double)pageSize); // Correcting the division to avoid integer division

            ViewData["AllInstructorNames"] = instructorRepository.GetAll().Select(i => i.Name).ToList();

            return View(paginatedInstructors);
        }

        public IActionResult AllPartial(int page = 1, int pageSize = 3)
        {
            int skipCount = (page - 1) * pageSize;

            List<Instructor> paginatedInstructors = instructorRepository.GetPageList(skipCount, pageSize);


            int totalCount = instructorRepository.GetAll().Count();

            ViewData["TotalPages"] = Math.Ceiling(totalCount / (double)pageSize); // Correcting the division to avoid integer division

            List<string> allInstructorNames = instructorRepository.GetAll().Select(i => i.Name).ToList();
            ViewData["AllInstructorNames"] = allInstructorNames; // Pass the list directly to the ViewData

            return PartialView("_InsTablePartial", paginatedInstructors);
        }


        public IActionResult Details(int id)
        {
            Instructor instructor = instructorRepository.GetById(id , DepartmentIncludeOptions.Include , CourseIncludeOptions.Include);

            return View(instructor);
        }

        [Authorize(Roles ="Admin")]
        public IActionResult New(Ins_With_Departments_Courses_IDs_Names PrevViewModel)
        {
            //if (PrevViewModel.Name == null || PrevViewModel.Salary == null || PrevViewModel.Adress == null || PrevViewModel.ImageUrl == null)
            if (PrevViewModel.Departments.Count == 0)  // the first time
            {
                Ins_With_Departments_Courses_IDs_Names viewModel =
                new Ins_With_Departments_Courses_IDs_Names();

                viewModel.Departments = departmentRepository.GetAll();
                viewModel.Courses =  courseRepository.GetAll();

                return View(viewModel);
            }
            else
            {
                return View(PrevViewModel);
            }
            #region old view data
            //--------------------------

            //storing view data recieved from the (SaveNew) Action
            // but if this action is used directly being redirected to => then view data would be null
            // and I am handling the null in the view with collease operator (??)

            //ViewData["tempInsName"] = TempData["tempInsName"];
            //ViewData["tempInsAdress"] = TempData["tempInsAdress"];


            //string tempSal = TempData["tempInsSalary"] as string;
            //if (decimal.TryParse(tempSal, out decimal sal))
            //{
            //    ViewData["tempInsSalary"] = sal;
            //}
            //else
            //{
            //    // would be null => the view will handle him and replace it with ""
            //}

            //ViewData["tempInsImg"] = TempData["tempInsImg"];

            //-------------------------- 
            #endregion
        }

        [HttpPost]
        public IActionResult SaveNew(Ins_With_Departments_Courses_IDs_Names newInsModel)
        {
            if (newInsModel.Name != null && newInsModel.Salary != null && newInsModel.Adress != null && newInsModel.ImageUrl != null)
            {
                Instructor newIns = new Instructor();

                newIns.Id = newInsModel.Id;
                newIns.Name = newInsModel.Name;
                newIns.Salary = newInsModel.Salary;
                newIns.Adress = newInsModel.Adress;
                newIns.ImageUrl = newInsModel.ImageUrl;

                newIns.DepartmentId = newInsModel.DepartmentId;
                newIns.CourseId = newInsModel.CourseId;

                newIns.Course = courseRepository.GetById(newInsModel.CourseId);
                newIns.Department = departmentRepository.GetById(newInsModel.DepartmentId);

                instructorRepository.Insert(newIns);

                courseRepository.Save();
                instructorRepository.Save();
                departmentRepository.Save();

                return RedirectToAction("All");
            }
            else
            {
                #region old temp data
                // Note : u have to store in temp data not view data => because When you use RedirectToAction,
                // it causes a redirect to a different action method,
                // and ViewData is not preserved between requests.


                //TempData, unlike ViewData, persists data for the duration of a single HTTP request,
                //making it suitable for scenarios where you need to 
                //pass data between different action methods during redirects.

                //TempData["tempInsName"] = newIns.Name;
                //TempData["tempInsAdress"] = newIns.Adress;
                //TempData["tempInsSalary"] = newIns.Salary.ToString();
                //TempData["tempInsImg"] = newIns.ImageUrl; 
                #endregion

                Ins_With_Departments_Courses_IDs_Names viewModel = new Ins_With_Departments_Courses_IDs_Names();

                viewModel.Id = newInsModel.Id;
                viewModel.Name = newInsModel.Name;
                viewModel.Adress = newInsModel.Adress;
                viewModel.ImageUrl = newInsModel.ImageUrl;
                viewModel.Salary = newInsModel.Salary;

                viewModel.DepartmentId = newInsModel.DepartmentId;
                viewModel.Department = newInsModel.Department;

                viewModel.CourseId = newInsModel.CourseId;
                viewModel.Course = newInsModel.Course;

                viewModel.Departments = departmentRepository.GetAll();
                viewModel.Courses = courseRepository.GetAll();

                return View("New", viewModel);
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Ins_With_Departments_Courses_IDs_Names prevViewModel)
        {
            #region old view data and temp data
            //if (id == 0) // handling the id in the redirection to this action from another action
            //{
            //    id = (int)TempData["editedInsID"];
            //    ViewData["insID"] = TempData["editedInsID"];

            //    ViewData["insName"] = TempData["editedInsName"];

            //    string tempSal = TempData["editedInsSalary"] as string;

            //    if (decimal.TryParse(tempSal, out decimal sal))
            //    {
            //        ViewData["insSalary"] = sal;
            //    }
            //    else
            //    {
            //        // would be null => the view will handle him and replace it with "" using collease operator (??)
            //    }

            //    ViewData["insAdress"] = TempData["editedInsAdress"];
            //    ViewData["insDeptID"] = TempData["editedInsDeptID"];
            //    ViewData["insCrsID"] = TempData["editedInsCrsID"];
            //    ViewData["insImg"] = TempData["editedInsImg"];
            //}
            //else
            //{ 
            #endregion

            // the firt time logging the action then the view model would take the default values
            // and I can check on depId or crsId because they are comboBox and will always have values
            // and these values will never be zero because they are entities ("1","1")

            if (prevViewModel.DepartmentId != 0)
            {
                return View(prevViewModel);
            }
            else
            {
                //Instructor instructor = context.Instructors.FirstOrDefault(i => i.Id == prevViewModel.Id);
                Instructor instructor = instructorRepository.GetById(prevViewModel.Id);

                Ins_With_Departments_Courses_IDs_Names viewModel = new Ins_With_Departments_Courses_IDs_Names();

                viewModel.Id = instructor.Id;
                viewModel.Name = instructor.Name;
                viewModel.Adress = instructor.Adress;
                viewModel.Salary = instructor.Salary;
                viewModel.ImageUrl = instructor.ImageUrl;

                viewModel.DepartmentId = instructor.DepartmentId;
                viewModel.CourseId = instructor.CourseId;

                viewModel.Departments = departmentRepository.GetAll();
                viewModel.Courses = courseRepository.GetAll();

                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult SaveEdited(Instructor editedIns)
        {
            if (editedIns != null && editedIns.Salary != null && editedIns.Adress != null && editedIns.Name != null && editedIns.Id != null && editedIns.ImageUrl != null)
            {
                Instructor originalIns = instructorRepository.GetById(editedIns.Id);

                originalIns.Name = editedIns.Name;
                originalIns.Adress = editedIns.Adress;
                originalIns.Salary = editedIns.Salary;
                originalIns.ImageUrl = editedIns.ImageUrl;

                originalIns.DepartmentId = editedIns.DepartmentId;
                originalIns.Department = departmentRepository.GetById(editedIns.DepartmentId);

                originalIns.CourseId = editedIns.CourseId;
                originalIns.Course = courseRepository.GetById(editedIns.CourseId);

                instructorRepository.Save();
                departmentRepository.Save();
                courseRepository.Save();

                return RedirectToAction("All");

            }
            else
            {
                #region old temp data
                // have to send the id to Edit because it will be Zero this time

                //TempData["editedInsID"] = editedIns.Id;
                //TempData["editedInsName"] = editedIns.Name;
                //TempData["editedInsAdress"] = editedIns.Adress;
                //TempData["editedInsSalary"] = editedIns.Salary.ToString(); // tempdata cant hold decimal so u have to convert it to string or jason
                //TempData["editedInsDeptID"] = editedIns.DepartmentId;
                //TempData["editedInsCrsID"] = editedIns.CourseId;
                //TempData["editedInsImg"] = editedIns.ImageUrl; 
                #endregion

                Ins_With_Departments_Courses_IDs_Names viewModel = new Ins_With_Departments_Courses_IDs_Names();

                viewModel.Id = editedIns.Id;
                viewModel.Name = editedIns.Name;
                viewModel.Adress = editedIns.Adress;
                viewModel.Salary = editedIns.Salary;
                viewModel.ImageUrl = editedIns.ImageUrl;

                viewModel.DepartmentId = editedIns.DepartmentId;
                viewModel.CourseId = editedIns.CourseId;

                viewModel.Departments = departmentRepository.GetAll();
                viewModel.Courses = courseRepository.GetAll();

                return View("Edit", viewModel);
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            ViewData["id"] = id;
            return View("Delete");
        }

        public IActionResult SaveDelete(int id)
        {
            Instructor deletedInd = instructorRepository.GetById(id);

            if (deletedInd != null)
            {
                instructorRepository.Delete(deletedInd.Id);

                instructorRepository.Save();
                courseRepository.Save();
                departmentRepository.Save();

                return RedirectToAction("All");
            }
            else
            {
                return RedirectToAction("All");
            }
        }

        public IActionResult Search(string searchInsName)
        {
            List<Instructor> filteredInstructors = instructorRepository.GetAll(DepartmentIncludeOptions.Include , CourseIncludeOptions.Include)
                .Where(i => i.Name.Contains(searchInsName)).ToList();


            List<string> allInstructorNames = instructorRepository.GetAll().Select(i => i.Name).ToList();
            ViewData["AllInstructorNames"] = allInstructorNames; // Pass the list directly to the ViewData

            return View("All", filteredInstructors);
        }
    }
}
