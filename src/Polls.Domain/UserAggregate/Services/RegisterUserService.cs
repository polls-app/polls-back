using Polls.Domain.ProfileAggregate;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Domain.UserAggregate.Services;

public class RegisterUserService
{
    public (User user, Profile profile) Register(Email email, Password password)
    {
        var user = User.Register(email, password);
        var profile = Profile.CreateDefault(user.Id);

        return (user, profile);
    }
}