using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcApp.Models
{
    [Table("STUDENTS")]
    public class Student
    {
        [Key]
        public int STUDENT_ID { get; set; }
        [Required]
        public int GROUP_ID { get; set; }
        [Required]
        public string FIRST_NAME { get; set; } = null!;
        [Required]
        public string LAST_NAME { get; set; } = null!;

        [ForeignKey("GROUP_ID")]
        public Group? Group { get; set; }
    }
}
