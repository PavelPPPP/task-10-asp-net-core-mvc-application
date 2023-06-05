using Core.ValueObjects;

namespace Core.Entities
{
    public class Course : Entity
    {
        public DataCourse? DataCourse { get; private set; }
        public List<Group> Groups { get; protected set; } = new();

        protected Course() : base() { }

        public Course(DataCourse dataCourse) : this()
        {
            ValidateArguments(dataCourse);

            DataCourse = dataCourse;
        }

        public void Change(DataCourse dataCourse)
        {
            ValidateArguments(dataCourse);

            DataCourse = dataCourse;
        }

        private void ValidateArguments(DataCourse dataCourse)
        {
            if (dataCourse is null)
            {
                throw new ArgumentNullException(nameof(dataCourse));
            }
        }
    }
}