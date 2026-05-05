using Core.DataSource;
using Core.Repositoties;
using Core.Entities;
using MvcApp.Models;
using Core.ValueObjects;

namespace MvcApp.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentRepository<Student> _studentRepository;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            _studentRepository = unitOfWork.Students;
        }

        public async Task<IEnumerable<StudentModel>> GetStudentsOfGroup(int? groupId)
        {
            if (groupId is null)
            {
                throw new ArgumentNullException(nameof(groupId));
            }

            var students = await _studentRepository.GetStudentsOfGroup(groupId, StudentModel.StudentSelector);

            return students;
        }

        public async Task<StudentModel> GetStudentModel(int? studentId)
        {
            if (studentId is null)
            {
                throw new ArgumentNullException(nameof(studentId));
            }

            var studentModel = await _studentRepository.GetWithProjection(studentId, StudentModel.StudentSelector);

            return studentModel;
        }

        public async Task CreateStudent(StudentModel studentModel)
        {
            if (studentModel is null)
            {
                throw new ArgumentNullException(nameof(studentModel));
            }

            var student = new Student(studentModel.GroupId, new StudentName(new Name(studentModel.FirstName), new Name(studentModel.LastName)));

            await _studentRepository.Create(student);
            await _unitOfWork.Save();
        }

        public async Task UpdateStudent(StudentModel studentModel)
        {
            if (studentModel is null)
            {
                throw new ArgumentNullException(nameof(studentModel));
            }

            var student = await _studentRepository.Get(studentModel.StudentId);
            student.Change(studentModel.GroupId, new StudentName(new Name(studentModel.FirstName), new Name(studentModel.LastName)));

            _studentRepository.Update(student);
            await _unitOfWork.Save();
        }

        public async Task DeleteStudent(int? studentId)
        {
            if (studentId is null)
            {
                throw new ArgumentNullException(nameof(studentId));
            }

            var student = await _studentRepository.Get(studentId);

            _studentRepository.Delete(student);
            await _unitOfWork.Save();
        }
    }
}
