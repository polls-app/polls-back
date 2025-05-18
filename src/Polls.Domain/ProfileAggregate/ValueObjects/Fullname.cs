namespace Polls.Domain.ProfileAggregate.ValueObjects;

public record Fullname(Name Firstname, Name Lastname)
{
    public static Fullname Empty => CreateEmpty();

    public static Fullname CreateEmpty()
        => new(new Name(string.Empty), new Name(string.Empty));
}