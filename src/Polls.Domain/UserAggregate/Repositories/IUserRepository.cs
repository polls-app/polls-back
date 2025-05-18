using Polls.Domain.ProfileAggregate;
using Polls.Domain.ProfileAggregate.ValueObjects;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Domain.UserAggregate.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(Email email);

    Task<bool> IsEmailTakenAsync(Email email);

    Task<bool> IsUsernameTakenAsync(Username username);

    Task<bool> ExistsAsync(UserId userId);

    Task AddAsync(User user, Profile profile);

    Task UpdateAsync(User user);
}