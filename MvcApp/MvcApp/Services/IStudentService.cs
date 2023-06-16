using MvcApp.Models;

namespace MvcApp.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentModel>> GetStudentsOfGroup(int? groupId);
        Task<StudentModel> GetStudentModel(int? id);
        Task CreateStudent(StudentModel studentModel);
        Task UpdateStudent(StudentModel studentModel);
        Task DeleteStudent(int? id);
    }
}
