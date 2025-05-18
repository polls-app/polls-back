using System.Security.Claims;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Api.Extractors;

public interface IUserExtractor
{
    UserId GetId();

    Email GetEmail();
}

public sealed class UserExtractor(IHttpContextAccessor httpContextAccessor) : IUserExtractor
{
    public UserId GetId()
    {
        var userId = httpContextAccessor.HttpContext?.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value
            ?? throw new ApplicationException("No userId claim found");

        return new UserId(new Guid(userId));
    }

    public Email GetEmail()
    {
        var email = httpContextAccessor.HttpContext?.User.Claims.First(c => c.Type == ClaimTypes.Email).Value
            ?? throw new ApplicationException("No email claim found");

        return new Email(email);
    }
}