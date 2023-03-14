using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcApp.Models
{
    [Table("COURSES")]
    public class Course
    {
        [Key]
        public int COURSE_ID { get; set; }
        [Required]
        public string NAME { get; set; } = null!;
        public string? DESCRIPTION { get; set; }

        public List<Group> Groups { get; set; } = new();
    }
}
