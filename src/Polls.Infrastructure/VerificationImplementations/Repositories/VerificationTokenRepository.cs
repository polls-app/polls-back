using Microsoft.Extensions.Caching.Memory;
using Polls.Domain.UserAggregate.ValueObjects;
using Polls.Domain.Verification;
using Polls.Domain.Verification.Repositories;

namespace Polls.Infrastructure.VerificationImplementations.Repositories;

public class VerificationTokenRepository(IMemoryCache cache) : IVerificationTokenRepository
{
    public Task Store(Email email, VerificationToken token)
    {
        cache.Set(email.Value, token, token.ExpiresAt);
        return Task.CompletedTask;
    }

    public Task<VerificationToken?> Get(Email email)
    {
        cache.TryGetValue(email.Value, out VerificationToken? token);
        return Task.FromResult(token);
    }
}