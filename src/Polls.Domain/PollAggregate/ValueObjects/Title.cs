namespace Polls.Domain.PollAggregate.ValueObjects;

public sealed class Title
{
    public Title(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }
}