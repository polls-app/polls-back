namespace Polls.Domain.ProfileAggregate.ValueObjects;

public class Avatar
{
    public Avatar(string path)
    {
        Path = path;
    }

    public string Path { get; private set; }

    public static Avatar Default => CreateDefault();

    public static Avatar CreateDefault() => new("https://www.shutterstock.com/image-vector/default-avatar-profile-icon-vector-600nw-1725655669.jpg");
}