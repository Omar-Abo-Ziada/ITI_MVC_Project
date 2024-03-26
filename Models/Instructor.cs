using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstitueProject.Models
{
    [Table("Instructor")]
    public class Instructor
    {
        [Key]
        public int Id { get; set; }

        [Required , MaxLength(50)]
        public string Name{ get; set; }

        [MaxLength(100)]
        public string? Adress { get; set; }

        [MaxLength(100)]
        public string? ImageUrl { get; set; }

        [Column(TypeName = "money")]
        public decimal? Salary { get; set; }

        //-------------------------------------
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        //-------------------------------------
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

    }
}
