namespace Core.ValueObjects
{
    public class DataCourse : ValueObject
    {
        public string? Name { get; private set; }
        public string? Description { get; private set; }

        protected DataCourse() { }
        public DataCourse(string? name, string? description)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            Description = description;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Description;
        }
    }
}
