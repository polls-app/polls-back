namespace Polls.Domain.Base.ValueObjects;

public abstract class StronglyTypedId<T> : IEquatable<StronglyTypedId<T>>
    where T : StronglyTypedId<T>
{
    public Guid Value { get; }

    protected StronglyTypedId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException($"{typeof(T).Name} cannot be empty");

        Value = value;
    }

    public override bool Equals(object? obj) =>
        obj is StronglyTypedId<T> other && Value == other.Value;

    public bool Equals(StronglyTypedId<T>? other) =>
        other is not null && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString();

    public static bool operator ==(StronglyTypedId<T> a, StronglyTypedId<T> b) =>
        Equals(a, b);

    public static bool operator !=(StronglyTypedId<T> a, StronglyTypedId<T> b) =>
        !Equals(a, b);
}
