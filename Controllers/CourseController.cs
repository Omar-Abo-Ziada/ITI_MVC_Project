using InstitueProject.Enums;
using InstitueProject.Models;
using InstitueProject.Repository;
using InstitueProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using InstitueProject.Filters;
using Microsoft.AspNetCore.Authorization;

namespace InstitueProject.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        //ITIContext context = new ITIContext();
        IDepartmentRepository departmentRepository;
        IInstructorRepository instructorRepository;
        ICourseRepository courseRepository;

        public CourseController(IDepartmentRepository deptRepo , IInstructorRepository insRepo , ICourseRepository crsRepo)
        {
            departmentRepository = deptRepo;
            instructorRepository = insRepo;
            courseRepository = crsRepo;
        }

       //[HandleError]
        public IActionResult All(int page = 1, int pageSize = 5)    
        {
            //throw new Exception("Omar Exception");
            int SkipStep = (page - 1) * pageSize;

            List<Course> PaginatedCourses = courseRepository.GetPageList(SkipStep , pageSize);

            int coursesCount = courseRepository.GetAll().Count();

            ViewData["TotalPages"] = Math.Ceiling(coursesCount / 5d);

            ViewData["AllCoursesNames"] = courseRepository.GetAll().Select(c => c.Name).ToList();

            return View(PaginatedCourses);
        }

        public IActionResult AllPartial(int page = 1, int pageSize = 5)
        {
            int SkipStep = (page - 1) * pageSize;

            List<Course> PaginatedCourses = courseRepository.GetPageList(SkipStep , pageSize);

            int coursesCount = courseRepository.GetAll().Count();

            ViewData["TotalPages"] = Math.Ceiling(coursesCount / 5d);

            ViewData["AllCoursesNames"] = courseRepository.GetAll().Select(c => c.Name).ToList() ;

            return PartialView("_CrsTablePartial", PaginatedCourses);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult New(CourseViewModel prevModel)
        {
            if (prevModel.Departments != null)
            {
                CourseViewModel model = new CourseViewModel();

                model.Departments = departmentRepository.GetAll(); // cmbox => he will choose one dep
                model.Instructors = instructorRepository.GetAll(); // checkbox => he may choose more than one

                return View(model);
            }
            else
            {

                prevModel.Departments = departmentRepository.GetAll(); // cmbox => he will choose one dep
                prevModel.Instructors = instructorRepository.GetAll(); // checkbox => he may choose more than one


                return View(prevModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(CourseViewModel model)
        {
            //if (model.Name != null
            //  && model.Hours != null && model.Hours > 0
            //  && model.Degree != null && model.Degree > 0
            //  && model.MinDegree != null && model.MinDegree > 0
            //  && model.SelectedInstructorIds.Count > 0)

            if (ModelState.IsValid)
            {
                Course course = new Course();

                course.Name = model.Name;
                course.Hours = model.Hours;
                course.Degree = model.Degree;
                course.MinDegree = model.MinDegree;

                course.DepartmentId = model.DepartmentId;
                course.Department = departmentRepository.GetById(model.DepartmentId);

                course.Instructors = instructorRepository.GetAll().Where(i => model.SelectedInstructorIds.Contains(i.Id)).ToList();

                foreach (Instructor ins in course.Instructors)
                {
                    ins.CourseId = course.Id;
                    ins.Course = course;

                    ins.DepartmentId = model.DepartmentId;
                    ins.Department = course.Department;
                }

                courseRepository.Insert(course);

                courseRepository.Save();
                instructorRepository.Save();
                departmentRepository.Save();
                

                return RedirectToAction("All");
            }
            else
            {
                model.Departments = departmentRepository.GetAll();  // cmbox => he will choose one dep
                model.Instructors = instructorRepository.GetAll();  // checkbox => he may choose more than one

                return View("New", model);
            }
        }

        public IActionResult CheckMinDegree(int Degree, int MinDegree)
        {
            if (MinDegree < Degree)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(CourseViewModel model) // take the Id only at the first time
        {
            Course crs = courseRepository.GetById(model.Id);

            if (crs != null)
            {
                model.Name = crs.Name;
                model.Hours = crs.Hours;
                model.Degree = crs.Degree;
                model.MinDegree = crs.MinDegree;

                model.DepartmentId = crs.DepartmentId;
                //firstModel.Department = context.Department.FirstOrDefault(d => d.Id == firstModel.DepartmentId);
                model.Department = departmentRepository.GetById(model.DepartmentId);

                //firstModel.SelectedInstructorIds = context.Instructors.                // display the selected instructors
                //                                   Where(i => i.CourseId == firstModel.Id).
                //                                   Select(i => i.Id).ToList();

                model.SelectedInstructorIds = instructorRepository.GetAll()                // display the selected instructors
                                                   .Where(i => i.CourseId == model.Id).
                                                   Select(i => i.Id).ToList();


                model.Instructors = instructorRepository.GetAll();        // all the instructors to display them in the checkbox
                model.Departments = departmentRepository.GetAll();       // all the deps to display them in the combox

                return View(model);
            }
            else
            {
                return RedirectToAction("All");
            }
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult SaveEdit(CourseViewModel model)
        {

            // TODO: Refactor this action because there are some unexpected behaviours when adding or removing instructors


            //if (model.Name != null
            //    && model.Hours != null && model.Hours > 0
            //    && model.Degree != null && model.Degree > 0
            //    && model.MinDegree != null && model.MinDegree > 0
            //    //&& model.SelectedInstructorIds.Count > 0)  may be he want to change another thing 
            //    )
            if (ModelState.IsValid)
            {
                //Course course = context.Courses.FirstOrDefault(c => c.Id == model.Id);
                Course course = courseRepository.GetById(model.Id);

                course.Name = model.Name;
                course.Hours = model.Hours;
                course.Degree = model.Degree;
                course.MinDegree = model.MinDegree;

                course.DepartmentId = model.DepartmentId;
                //course.Department = context.Department.FirstOrDefault(d => d.Id == model.DepartmentId);
                course.Department = departmentRepository.GetById(model.DepartmentId);

                //foreach (Instructor ins in context.Instructors.Where(i => i.CourseId==course.Id).ToList())
                //{
                //      // SQL Exception
                //    // can't do because the FK doesn't allow null
                //    // each instructor has to be assigned in a course

                //    ins.Course = null;
                //    ins.CourseId = null ; 
                //}

                //course.Instructors = context.Instructors.Where(i => model.SelectedInstructorIds.Contains(i.Id)).ToList();
                
                course.Instructors = instructorRepository.GetAll().Where(i => i.Id == model.Id).ToList();

                foreach (Instructor ins in instructorRepository.GetAll())
                {
                    if (model.SelectedInstructorIds.Contains(ins.Id))
                    {
                        course.Instructors.Add(ins);
                    }
                }


                foreach (Instructor ins in course.Instructors)
                {
                    ins.CourseId = course.Id;
                    ins.Course = course;

                    ins.DepartmentId = model.DepartmentId;
                    ins.Department = departmentRepository.GetById(model.DepartmentId);
                }

                courseRepository.Save();
                departmentRepository.Save();
                instructorRepository.Save();

                return RedirectToAction("All");

            }
            else
            {
                return RedirectToAction("Edit", model);
            }
        }



        //==============================================================
        //[Authorize(Roles = "Admin")]
        //public IActionResult Edit(CourseViewModel model) // take the Id only at the first time
        //{
        //    Course crs = courseRepository.GetById(model.Id);

        //    if (crs != null)
        //    {
        //        CourseViewModel firstModel = new CourseViewModel();

        //        firstModel.Id = crs.Id;
        //        firstModel.Name = crs.Name;
        //        firstModel.Hours = crs.Hours;
        //        firstModel.Degree = crs.Degree;
        //        firstModel.MinDegree = crs.MinDegree;

        //        firstModel.DepartmentId = crs.DepartmentId;
        //        //firstModel.Department = context.Department.FirstOrDefault(d => d.Id == firstModel.DepartmentId);
        //        firstModel.Department = departmentRepository.GetById(firstModel.DepartmentId);

        //        //firstModel.SelectedInstructorIds = context.Instructors.                // display the selected instructors
        //        //                                   Where(i => i.CourseId == firstModel.Id).
        //        //                                   Select(i => i.Id).ToList();

        //        firstModel.SelectedInstructorIds = instructorRepository.GetAll()                // display the selected instructors
        //                                           .Where(i => i.CourseId == firstModel.Id).
        //                                           Select(i => i.Id).ToList();



        //        firstModel.Instructors = instructorRepository.GetAll();        // all the instructors to display them in the checkbox
        //        firstModel.Departments = departmentRepository.GetAll();       // all the deps to display them in the combox

        //        return View(firstModel);
        //    }
        //    else
        //    {
        //        return RedirectToAction("All");
        //    }
        //}


        //[HttpPost]
        //[AutoValidateAntiforgeryToken]
        //public IActionResult SaveEdit(CourseViewModel model)
        //{
        //    //if (model.Name != null
        //    //    && model.Hours != null && model.Hours > 0
        //    //    && model.Degree != null && model.Degree > 0
        //    //    && model.MinDegree != null && model.MinDegree > 0
        //    //    //&& model.SelectedInstructorIds.Count > 0)  may be he want to change another thing 
        //    //    )
        //    if (ModelState.IsValid)
        //    {
        //        //Course course = context.Courses.FirstOrDefault(c => c.Id == model.Id);
        //        Course course = courseRepository.GetById(model.Id);

        //        course.Name = model.Name;
        //        course.Hours = model.Hours;
        //        course.Degree = model.Degree;
        //        course.MinDegree = model.MinDegree;

        //        course.DepartmentId = model.DepartmentId;
        //        //course.Department = context.Department.FirstOrDefault(d => d.Id == model.DepartmentId);
        //        course.Department = departmentRepository.GetById(model.DepartmentId);

        //        //foreach (Instructor ins in context.Instructors.Where(i => i.CourseId==course.Id).ToList())
        //        //{
        //        //      // SQL Exception
        //        //    // can't do because the FK doesn't allow null
        //        //    // each instructor has to be assigned in a course

        //        //    ins.Course = null;
        //        //    ins.CourseId = null ; 
        //        //}

        //        //course.Instructors = context.Instructors.Where(i => model.SelectedInstructorIds.Contains(i.Id)).ToList();
        //        course.Instructors = instructorRepository.GetAll().Where(i => model.SelectedInstructorIds.Contains(i.Id)).ToList();
        //        foreach (Instructor ins in course.Instructors)
        //        {
        //            ins.CourseId = course.Id;
        //            ins.Course = course;

        //            ins.DepartmentId = model.DepartmentId;
        //            //ins.Department = context.Department.FirstOrDefault(d => d.Id == model.DepartmentId);
        //            ins.Department = departmentRepository.GetById(model.DepartmentId);
        //        }

        //        //context.SaveChanges();
        //        courseRepository.Save();
        //        departmentRepository.Save();
        //        instructorRepository.Save();

        //        return RedirectToAction("All");

        //    }
        //    else
        //    {
        //        return RedirectToAction("Edit", model);
        //    }
        //}
        //=============================================================

        public IActionResult Details(CourseViewModel model)
        {
            Course crs = courseRepository.GetById(model.Id, DepartmentIncludeOptions.Include, InstructorsIncludeOptions.Include);

            if (crs != null)
            {
                model.Id = crs.Id;
                model.Name = crs.Name;
                model.Hours = crs.Hours;
                model.Degree = crs.Degree;
                model.MinDegree = crs.MinDegree;
                model.DepartmetName = crs.Department.Name;

                model.DepartmentId = crs.DepartmentId;
                model.Department = crs.Department;

                model.Instructors = crs.Instructors.ToList();

                return View(model);
            }
            else
            {
                return RedirectToAction("All");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            ViewData["id"] = id;
            return View("Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult SaveDelete(int id)
        {
            Course deletedCrs = courseRepository.GetById(id , DepartmentIncludeOptions.None , InstructorsIncludeOptions.Include );

            if (deletedCrs.Instructors.Count == 0)  // checking there are no instructors assigned in
            {
                courseRepository.Delete(deletedCrs.Id);

                courseRepository.Save();

                return RedirectToAction("All");
            }
            else
            {
                ViewData["InsCount"] = deletedCrs.Instructors.Count;

                return View("SaveDelete");  // telling him that he cant delete crs because there are ins in it
            }
        }

        public IActionResult Search(string searchCrsName)
        {
            List<Course> filteredCourses = courseRepository.GetAll(DepartmentIncludeOptions.Include)
                .Where(i => i.Name.Contains(searchCrsName)).ToList();

            ViewData["AllCoursesNames"] =courseRepository.GetAll().Select(c => c.Name).ToList();


            return View("All", filteredCourses);
        }

        public IActionResult GetCoursesByDeptId(int deptid)
        {
            var courses = courseRepository.GetAll()
                                   .Where(c => c.DepartmentId == deptid)
                                   .Select(c => new { c.Name, c.Id }).ToList();

            return Json(courses);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ShowCourseResults(Course crs)
        {
            List<TraineeName_CourseName_Degree_IsPassed> viewModels =
                courseRepository.ShowCourseResults(crs);

            return View(viewModels);
        }

        //=======================================================

        #region old without repo


        //public IActionResult All(int page = 1, int pageSize = 5)
        //{
        //    int SkipStep = (page - 1) * pageSize;

        //    List<Course> PaginatedCourses = context.Courses.Skip(SkipStep).Take(pageSize).ToList();

        //    int coursesCount = context.Courses.Count();

        //    ViewData["TotalPages"] = Math.Ceiling(coursesCount / 5d);

        //    ViewData["AllCoursesNames"] = context.Courses.Select(c => c.Name).ToList();

        //    return View(PaginatedCourses);
        //}

        //public IActionResult AllPartial(int page = 1, int pageSize = 5)
        //{
        //    int SkipStep = (page - 1) * pageSize;

        //    List<Course> PaginatedCourses = context.Courses.Skip(SkipStep).Take(pageSize).ToList();

        //    int coursesCount = context.Courses.Count();

        //    ViewData["TotalPages"] = Math.Ceiling(coursesCount / 5d);

        //    ViewData["AllCoursesNames"] = context.Courses.Select(c => c.Name).ToList();

        //    return PartialView("_CrsTablePartial", PaginatedCourses);
        //}


        //public IActionResult New(CourseViewModel prevModel)
        //{
        //    if (prevModel.Departments != null)
        //    {
        //        CourseViewModel model = new CourseViewModel();

        //        model.Departments = context.Department.ToList(); // cmbox => he will choose one dep
        //        model.Instructors = context.Instructors.ToList(); // checkbox => he may choose more than one

        //        return View(model);
        //    }
        //    else
        //    {

        //        prevModel.Departments = context.Department.ToList(); // cmbox => he will choose one dep
        //        prevModel.Instructors = context.Instructors.ToList(); // checkbox => he may choose more than one

        //        return View(prevModel);
        //    }
        //}

        //[HttpPost]
        //[AutoValidateAntiforgeryToken]
        //public IActionResult SaveNew(CourseViewModel model)
        //{
        //    //if (model.Name != null
        //    //  && model.Hours != null && model.Hours > 0
        //    //  && model.Degree != null && model.Degree > 0
        //    //  && model.MinDegree != null && model.MinDegree > 0
        //    //  && model.SelectedInstructorIds.Count > 0)
        //    if (ModelState.IsValid)
        //    {
        //        Course course = new Course();

        //        course.Name = model.Name;
        //        course.Hours = model.Hours;
        //        course.Degree = model.Degree;
        //        course.MinDegree = model.MinDegree;

        //        course.DepartmentId = model.DepartmentId;
        //        course.Department = context.Department.FirstOrDefault(d => d.Id == model.DepartmentId);

        //        course.Instructors = context.Instructors.Where(i => model.SelectedInstructorIds.Contains(i.Id)).ToList();
        //        foreach (Instructor ins in course.Instructors)
        //        {
        //            ins.CourseId = course.Id;
        //            ins.Course = course;

        //            ins.DepartmentId = model.DepartmentId;
        //            ins.Department = course.Department;
        //        }

        //        context.Courses.Add(course);

        //        context.SaveChanges();

        //        return RedirectToAction("All");
        //    }
        //    else
        //    {
        //        model.Departments = context.Department.ToList(); // cmbox => he will choose one dep
        //        model.Instructors = context.Instructors.ToList(); // checkbox => he may choose more than one

        //        return View("New", model);
        //    }
        //}

        //public IActionResult CheckMinDegree(int Degree, int MinDegree)
        //{
        //    if (MinDegree < Degree)
        //    {
        //        return Json(true);
        //    }
        //    else
        //    {
        //        return Json(false);
        //    }
        //}


        ////         if (ModelState.IsValid == true)
        ////            {
        ////                try
        ////                {
        ////                    context.Add(empModelFromREquest);
        ////                    context.SaveChanges();
        ////                    return RedirectToAction("Index");
        ////    }
        ////                catch (Exception ex)
        ////                {
        ////                    //  ModelState.AddModelError("DepartmentId", "Please Select Department");
        ////                    ModelState.AddModelError("error", ex.InnerException.Message);
        ////                }
        ////            }
        ////            ViewData["DeptList"] = context.Department.ToList();
        ////return View("New", empModelFromREquest);//Model +List

        //public IActionResult Edit(CourseViewModel model) // take the Id only at the first time
        //{
        //    Course crs = context.Courses.FirstOrDefault(c => c.Id == model.Id);

        //    if (crs != null)
        //    {
        //        CourseViewModel firstModel = new CourseViewModel();

        //        firstModel.Id = crs.Id;
        //        firstModel.Name = crs.Name;
        //        firstModel.Hours = crs.Hours;
        //        firstModel.Degree = crs.Degree;
        //        firstModel.MinDegree = crs.MinDegree;

        //        firstModel.DepartmentId = crs.DepartmentId;
        //        firstModel.Department = context.Department.FirstOrDefault(d => d.Id == firstModel.DepartmentId);

        //        firstModel.SelectedInstructorIds = context.Instructors.                // display the selected instructors
        //                                           Where(i => i.CourseId == firstModel.Id).
        //                                           Select(i => i.Id).ToList();

        //        firstModel.Instructors = context.Instructors.ToList();            // all the instructors to display them in the checkbox
        //        firstModel.Departments = context.Department.ToList();            // all the deps to display them in the combox

        //        return View(firstModel);
        //    }
        //    else
        //    {
        //        return RedirectToAction("All");
        //    }
        //}

        //public IActionResult SaveEdit(CourseViewModel model)
        //{
        //    if (model.Name != null
        //        && model.Hours != null && model.Hours > 0
        //        && model.Degree != null && model.Degree > 0
        //        && model.MinDegree != null && model.MinDegree > 0
        //        //&& model.SelectedInstructorIds.Count > 0)  may be he want to change another thing 
        //        )
        //    {
        //        Course course = context.Courses.FirstOrDefault(c => c.Id == model.Id);

        //        course.Name = model.Name;
        //        course.Hours = model.Hours;
        //        course.Degree = model.Degree;
        //        course.MinDegree = model.MinDegree;

        //        course.DepartmentId = model.DepartmentId;
        //        course.Department = context.Department.FirstOrDefault(d => d.Id == model.DepartmentId);

        //        //foreach (Instructor ins in context.Instructors.Where(i => i.CourseId==course.Id).ToList())
        //        //{
        //        //      // SQL Exception
        //        //    // can't do because the FK doesn't allow null
        //        //    // each instructor has to be assigned in a course

        //        //    ins.Course = null;
        //        //    ins.CourseId = null ; 
        //        //}

        //        course.Instructors = context.Instructors.Where(i => model.SelectedInstructorIds.Contains(i.Id)).ToList();
        //        foreach (Instructor ins in course.Instructors)
        //        {
        //            ins.CourseId = course.Id;
        //            ins.Course = course;

        //            ins.DepartmentId = model.DepartmentId;
        //            ins.Department = context.Department.FirstOrDefault(d => d.Id == model.DepartmentId);
        //        }

        //        context.SaveChanges();

        //        return RedirectToAction("All");

        //    }
        //    else
        //    {
        //        return RedirectToAction("Edit", model);
        //    }
        //}

        //public IActionResult Details(CourseViewModel model)
        //{
        //    Course crs = context.Courses.Include(c => c.Department).Include(c => c.Instructors).FirstOrDefault(c => c.Id == model.Id);
        //    crs.Instructors = context.Instructors.Where(i => crs.Instructors.Contains(i)).Include(i => i.Department).ToList();  // getting the dep obj with each ins

        //    if (crs != null)
        //    {
        //        model.Id = crs.Id;
        //        model.Name = crs.Name;
        //        model.Hours = crs.Hours;
        //        model.Degree = crs.Degree;
        //        model.MinDegree = crs.MinDegree;
        //        model.DepartmetName = crs.Department.Name;

        //        model.DepartmentId = crs.DepartmentId;
        //        model.Department = crs.Department;

        //        model.Instructors = crs.Instructors.ToList();

        //        //model.SelectedInstructorIds = context.Instructors.                // display the selected instructors
        //        //                                   Where(i => i.CourseId == model.Id).
        //        //                                   Select(i => i.Id).ToList();

        //        return View(model);
        //    }
        //    else
        //    {
        //        return RedirectToAction("All");
        //    }
        //}

        //public IActionResult Delete(int id)
        //{
        //    ViewData["id"] = id;
        //    return View("Delete");
        //}

        //public IActionResult SaveDelete(int id)
        //{
        //    Course deletedCrs = context.Courses.Include(c => c.Instructors).FirstOrDefault(c => c.Id == id);

        //    if (deletedCrs.Instructors.Count == 0)  // checking there are no instructors assigned in
        //    {
        //        context.Courses.Remove(deletedCrs);
        //        context.SaveChanges();

        //        return RedirectToAction("All");
        //    }
        //    else
        //    {
        //        ViewData["InsCount"] = deletedCrs.Instructors.Count;

        //        return View("SaveDelete");  // telling him that he cant delete crs because there are ins in it
        //    }
        //}

        //public IActionResult Search(string searchCrsName)
        //{
        //    List<Course> filteredCourses = context.Courses
        //        .Where(i => i.Name.Contains(searchCrsName)).ToList();

        //    ViewData["AllCoursesNames"] = context.Courses.Select(c => c.Name).ToList();


        //    return View("All", filteredCourses);
        //}

        //public IActionResult GetCoursesByDeptId(int deptid)
        //{
        //    var courses = context.Courses
        //                           .Where(c => c.DepartmentId == deptid)
        //                           .Select(c => new { c.Name, c.Id }).ToList();

        //    return Json(courses);
        //}

        ////public IActionResult GetEmpByDEptID(int deptID)
        ////{
        ////    var employees = context.Employee
        ////        .Where(e => e.DepartmentId == deptID)
        ////        .Select(e => new { e.Name, e.Id }).ToList();
        ////    return Json(employees);
        ////} 
        #endregion

    }
}
