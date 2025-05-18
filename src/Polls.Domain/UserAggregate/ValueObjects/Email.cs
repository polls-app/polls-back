namespace Polls.Domain.UserAggregate.ValueObjects;

public sealed class Email
{
    public Email(string email)
    {
        Value = email;
    }

    public Email(string email, bool isVerified = false) : this(email)
    {
        IsVerified = isVerified;
    }

    public string Value { get; private set; }

    public bool IsVerified { get; private set; }

    public void Verify()
    {
        if (IsVerified)
            throw new ApplicationException("User is already verified");

        IsVerified = true;
    }
}