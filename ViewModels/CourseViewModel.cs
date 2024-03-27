using InstitueProject.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using InstitueProject.Authentication_Models;
using InstitueProject.Attributes;

namespace InstitueProject.ViewModels
{
    public class CourseViewModel
    {

        public List<Department>? Departments { get; set; }  // the lists to fill the comboBox

        public List<Instructor>? Instructors { get; set; }  // to fill the items in checkBox

        [Required]
        public List<int> SelectedInstructorIds { get; set; } = new List<int>(); // for the selected items in checkBox

        public string? DepartmetName { get; set; } // to use in details

        //===================================================
        // the primitive data types of course

        public int Id { get; set; }
        
        [Unique]
        [MinLength(2, ErrorMessage = "must be longer than 2 letters")]
        [MaxLength(20, ErrorMessage = "must be less than 20 letters")]
        public string Name { get; set; }

        [Hours]
        public int Hours { get; set; }

        [Range(50, 100)]
        public int Degree { get; set; }

        [Remote("CheckMinDegree", "Course"
            , ErrorMessage = "Min Degree must be less than the course Degree ", AdditionalFields = "Degree")]
        [Display(Name = "Minmum Degree")]
        public int MinDegree { get; set; }

        //-------------------------------------

        public int DepartmentId { get; set; }

        public virtual Department? Department { get; set; }

        //-------------------------------------

        //public virtual ICollection<Instructor> Instructors { get; set; }

        //-------------------------------------

        //public virtual ICollection<CourseResult> CourseResult { get; set; }

    }
}
