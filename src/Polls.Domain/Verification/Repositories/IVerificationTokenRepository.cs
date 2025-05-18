using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Domain.Verification.Repositories;

public interface IVerificationTokenRepository
{
    Task<VerificationToken?> Get(Email email);

    Task Store(Email email, VerificationToken token);
}