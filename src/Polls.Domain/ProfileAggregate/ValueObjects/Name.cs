namespace Polls.Domain.ProfileAggregate.ValueObjects;

public class Name
{
    public Name(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }
}