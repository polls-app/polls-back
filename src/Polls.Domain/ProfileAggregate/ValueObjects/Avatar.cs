namespace Polls.Domain.ProfileAggregate.ValueObjects;

public class Avatar
{
    public Avatar(string path)
    {
        Path = path;
    }

    public string Path { get; private set; }

    public static Avatar Default => CreateDefault();

    public static Avatar CreateDefault() => new(string.Empty);
}