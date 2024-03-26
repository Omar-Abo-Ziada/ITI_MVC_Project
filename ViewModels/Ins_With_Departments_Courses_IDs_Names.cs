using InstitueProject.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.Arm;

namespace InstitueProject.ViewModels
{
    public class Ins_With_Departments_Courses_IDs_Names
    {

        public List<Department> Departments { get; set; } = new List<Department>();

        public List<Course> Courses { get; set; } = new List<Course>();

        //=========================================
        // primitive data types of instructor

        public int Id { get; set; }

        public string Name { get; set; }

        public string? Adress { get; set; }

        public string? ImageUrl { get; set; }

        public decimal? Salary { get; set; }

        //-------------------------------------
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        //-------------------------------------
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        //=========================================


    }
}
