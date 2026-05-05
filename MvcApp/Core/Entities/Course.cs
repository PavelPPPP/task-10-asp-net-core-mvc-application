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
            DataCourse = dataCourse ?? throw new ArgumentNullException(nameof(dataCourse));
        }

        public void Change(DataCourse dataCourse)
        {
            DataCourse = dataCourse ?? throw new ArgumentNullException(nameof(dataCourse));
        }
    }
}