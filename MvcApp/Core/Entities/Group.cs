using Core.ValueObjects;

namespace Core.Entities
{
    public class Group : Entity
    {
        public int? CourseId { get; private set; }
        public Name? Name { get; private set; }

        public Course? Course { get; private set; }

        public List<Student> Students { get; private set; } = new();

        protected Group() : base() { }
        public Group(int? courseId, Name? name) : this()
        {
            ValidateArguments(courseId, name);

            CourseId = courseId;
            Name = name;
        }

        public void Change(int? courseId, Name? groupName)
        {
            ValidateArguments(courseId, groupName);

            CourseId = courseId;
            Name = groupName;
        }
        
        private void ValidateArguments(int? courseId, Name? groupName)
        {
            if (courseId is null)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            if (courseId <= 0)
            {
                throw new ArgumentException("CourseId could be is bigger then ziro!");
            }

            if (groupName is null)
            {
                throw new ArgumentNullException(nameof(groupName));
            }
        }
    }
}
