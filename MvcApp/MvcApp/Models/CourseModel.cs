using System.ComponentModel.DataAnnotations;

namespace MvcApp.Models
{
    public class CourseModel
    {
        public int COURSE_ID { get; set; }
        [Required]
        public string NAME { get; set; } = null!;
        public string? DESCRIPTION { get; set; }
    }
}
