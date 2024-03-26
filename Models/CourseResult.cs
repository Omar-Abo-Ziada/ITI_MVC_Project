using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstitueProject.Models
{
    [Table("CourseResult")]
    public class CourseResult
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Degree { get; set; }

        //------------------------------------
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        //------------------------------------
        [ForeignKey("Trainee")]
        public int TraineeId { get; set; }

        public virtual Trainee Trainee { get; set; }
    }
}
