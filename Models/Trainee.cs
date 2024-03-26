using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InstitueProject.Models
{
    [Table("Trainee")]
    public class Trainee
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string? ImageUrl { get; set; }

        [MaxLength(100)]
        public string? Adress { get; set; }

        public int?  Grade { get; set; }

        //-------------------------------------
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        //-------------------------------------

        public virtual ICollection<CourseResult> CourseResult { get; set; }
    }
}
