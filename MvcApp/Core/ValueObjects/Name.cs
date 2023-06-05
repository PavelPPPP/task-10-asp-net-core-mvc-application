namespace Core.ValueObjects
{
    public class Name : ValueObject
    {
        public string? Value { get; private set; }

        protected Name() { }
        public Name(string? value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value), "Name is not valid!");
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
