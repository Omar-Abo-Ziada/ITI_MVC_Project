using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstitueProject.Models
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string? ManagerName { get; set; }

        //----------------------------------------------------------

        public virtual ICollection<Instructor>? Instructors { get; set; }

        public virtual ICollection<Trainee>? Trainees { get; set; }

        public virtual ICollection<Course>? Courses { get; set; }

    }
}
