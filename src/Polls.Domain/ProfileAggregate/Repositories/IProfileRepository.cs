using Polls.Domain.ProfileAggregate.ValueObjects;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Domain.ProfileAggregate.Repositories;

public interface IProfileRepository
{
    Task<Profile?> GetProfileByUserId(UserId userId);

    Task<Username> GetUsernameById(UserId userId);

    Task UpdateProfile(Profile profile);

    Task<bool> IsUsernameTaken(Username username);
}