using Core.DataSource;
using Core.Entities;
using Core.Repositoties;
using Core.ValueObjects;
using MvcApp.Models;

namespace MvcApp.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository<Course> _courseRepository;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            
            _courseRepository = unitOfWork.Courses;
        }

        public async Task<IEnumerable<CourseModel>> GetAllCourses()
        {
            var courses = await _courseRepository.GetAllWithProjection(CourseModel.CourseSelector);

            return courses;
        }

        public async Task<CourseModel> Get(int? courseId)
        {
            if (courseId is null)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            var course = await _courseRepository.GetWithProjection(courseId, CourseModel.CourseSelector);

            return course;
        }

        public async Task<CourseModel> GetOnlyName(int? courseId)
        {
            if (courseId is null)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            var course = await _courseRepository.GetWithProjection(courseId, CourseModel.CourseNameSelector);

            return course;
        }

        public async Task CreateCourse(CourseModel courseModel)
        {
            if (courseModel is null)
            {
                throw new ArgumentNullException(nameof(courseModel));
            }

            var course = new Course(new DataCourse(courseModel.Name, courseModel.Description));

            await _courseRepository.Create(course);
            await _unitOfWork.Save();
        }

        public async Task UpdateCourse(CourseModel courseModel)
        {
            if (courseModel is null)
            {
                throw new ArgumentNullException(nameof(courseModel));
            }

            var course = await _courseRepository.Get(courseModel.CourseId);
            course.Change(new DataCourse(courseModel.Name, courseModel.Description));

            _courseRepository.Update(course);
            await _unitOfWork.Save();
        }

        public async Task DeleteCourse(int? courseId)
        {
            if (courseId is null)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            var course = await _courseRepository.GetWithDetail(courseId);

            if (course.Groups.Count() == 0)
            {
                _courseRepository.Delete(course);
                await _unitOfWork.Save();
            }
        }
    }
}
