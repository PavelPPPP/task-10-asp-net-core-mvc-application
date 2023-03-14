using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcApp.Models
{
    [Table("GROUPS")]
    public class Group
    {
        [Key]
        public int GROUP_ID { get; set; }
        [Required]
        public int COURSE_ID { get; set; }
        [Required]
        public string NAME { get; set; } = null!;

        [ForeignKey("COURSE_ID")]
        public Course? Course { get; set; }

        public List<Student> Students { get; set; } = new();
    }
}
