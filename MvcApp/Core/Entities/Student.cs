using Core.ValueObjects;

namespace Core.Entities
{
    public class Student : Entity
    {
        public int? GroupId { get; private set; }

        public StudentName StudentName { get; private set; }

        public Group? Group { get; private set; }

        protected Student() { }
        public Student(int? groupId, StudentName studentName) : this()
        {
            ValidateArguments(groupId, studentName);

            GroupId = groupId;
            StudentName = studentName;
        }

        public void Change(int? groupId, StudentName studentName)
        {
            ValidateArguments(groupId, studentName);

            GroupId = groupId;
            StudentName = studentName;
        }

        private void ValidateArguments(int? groupId, StudentName studentName)
        {
            if (groupId is null)
            {
                throw new ArgumentNullException(nameof(groupId));
            }

            if (groupId <= 0)
            {
                throw new ArgumentException("GroupId could be is bigger then ziro!");
            }

            if (studentName is null)
            {
                throw new ArgumentNullException(nameof(studentName));
            }
        }
    }
}
