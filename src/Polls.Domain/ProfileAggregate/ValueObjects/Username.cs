using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Domain.ProfileAggregate.ValueObjects;

public class Username
{
    public Username(string username)
    {
        Value = username;
    }

    public string Value { get; private set; }

    public static Username Generate()
        => new("user-" + Guid.NewGuid().ToString("N"));    
}