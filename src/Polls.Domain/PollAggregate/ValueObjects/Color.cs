namespace Polls.Domain.PollAggregate.ValueObjects;

public sealed class Color
{
    public Color(string colorHex)
    {
        Hex = colorHex;
    }

    public string Hex { get; private set; }
}
