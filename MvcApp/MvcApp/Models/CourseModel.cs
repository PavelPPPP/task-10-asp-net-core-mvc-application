using System.Linq.Expressions;

namespace MvcApp.Models
{
    public class CourseModel
    {
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public static Expression<Func<Core.Entities.Course, CourseModel>> CourseSelector
        {
            get
            {
                return course => new CourseModel()
                {
                    CourseId = course.Id,
                    Name = course.DataCourse.Name,
                    Description = course.DataCourse.Description
                };
            }
        }

        public static Expression<Func<Core.Entities.Course, CourseModel>> CourseNameSelector
        {
            get
            {
                return course => new CourseModel()
                {
                    CourseId = course.Id,
                    Name = course.DataCourse.Name
                };
            }
        }
    }
}
