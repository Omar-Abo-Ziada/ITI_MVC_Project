using InstitueProject.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstitueProject.Models
{
    [Table("Course")]
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Unique]
        [MinLength(2, ErrorMessage = "must be longer than 2 letters")]
        [MaxLength(20, ErrorMessage = "must be less than 20 letters")]
        public string Name { get; set; }

        public int Hours { get; set; }

        [Range(50, 100)]
        public int Degree { get; set; }

        [Display(Name = "Minmum Degree")]
        public int MinDegree { get; set; }

        //-------------------------------------

        [ForeignKey("Department")]
        [Display(Name = "Department ID")]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        //-------------------------------------

        public virtual ICollection<Instructor> Instructors { get; set; }

        //-------------------------------------

        public virtual ICollection<CourseResult> CourseResult { get; set; }

    }
}
