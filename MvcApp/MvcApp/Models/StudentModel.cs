using System.Linq.Expressions;

namespace MvcApp.Models
{
    public class StudentModel
    {
        public int StudentId { get; set; }
        public int? GroupId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static Expression<Func<Core.Entities.Student, StudentModel>> StudentSelector
        {
            get
            {
                return student => new StudentModel()
                {
                    StudentId = student.Id,
                    GroupId = student.GroupId,
                    FirstName = student.StudentName.FirstName.Value,
                    LastName = student.StudentName.LastName.Value                    
                };
            }
        }
    }
}
