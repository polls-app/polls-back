using Polls.Domain.Base;
using Polls.Domain.UserAggregate.Enums;
using Polls.Domain.UserAggregate.Events;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Domain.UserAggregate;

public sealed class User : AggregateRoot
{
    public User(
        UserId id,
        Email email,
        Password password,
        UserRole role,
        DateTime lastLoginAt,
        DateTime joinedAt)
    {
        Id = id;
        Email = email;
        Password = password;
        Role = role;
        LastLoginAt = lastLoginAt;
        JoinedAt = joinedAt;
    }

    public UserId Id { get; private set; }

    public Email Email { get; private set; }

    public Password Password { get; set; }

    public UserRole Role { get; private set; }

    public DateTime LastLoginAt { get; private set; }

    public DateTime JoinedAt { get; private set; }

    public void ChangePassword(Password newPassword) => Password = newPassword;

    public void UpdateLastLogin() => LastLoginAt = DateTime.UtcNow;

    public void PromoteToModerator() => Role = UserRole.Moderator;

    public void PromoteToAdmin() => Role = UserRole.Admin;

    public static User Register(Email email, Password password)
    {
        var user = new User(
            id: UserId.New(),
            email: email,
            password: password,
            role: UserRole.User,
            lastLoginAt: DateTime.UtcNow,
            joinedAt: DateTime.UtcNow);

        user.AddEvent(new UserRegistered(user.Email));
        return user;
    }
}