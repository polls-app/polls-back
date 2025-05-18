namespace Polls.Domain.PollAggregate.ValueObjects;

public class Language
{
    public Language(string code)
    {
        Code = code;
    }

    public string Code { get; private set; }
}