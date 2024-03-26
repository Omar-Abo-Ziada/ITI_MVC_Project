using InstitueProject.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InstitueProject.ViewModels
{
    public class TraineeViewModel
    {
        public List<Department> Departments { get; set; } = new List<Department>();

        //=====================================

        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string? ImageUrl { get; set; }

        [MaxLength(100)]
        public string Adress { get; set; }

        [Required]
        public int? Grade { get; set; }

        //-------------------------------------

        public int DepartmentId { get; set; }

        public virtual Department? Department { get; set; }

        //-------------------------------------

       // public virtual ICollection<CourseResult> CourseResult { get; set; }
    }
}
