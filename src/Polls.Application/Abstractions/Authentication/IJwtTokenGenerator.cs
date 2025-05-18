using Polls.Domain.UserAggregate;

namespace Polls.Application.Abstractions.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}