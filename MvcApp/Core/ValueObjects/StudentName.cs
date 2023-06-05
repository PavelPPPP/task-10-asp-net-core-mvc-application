namespace Core.ValueObjects
{
    public class StudentName : ValueObject
    {
        public Name FirstName { get; private set; }
        public Name LastName { get; private set; }

        protected StudentName() { }
        public StudentName(Name firstName, Name lastName) : this()
        {
            if (firstName is null)
            {
                throw new ArgumentNullException(nameof(firstName));
            }

            if (lastName is null)
            {
                throw new ArgumentNullException(nameof(lastName));
            }

            FirstName = firstName;
            LastName = lastName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
