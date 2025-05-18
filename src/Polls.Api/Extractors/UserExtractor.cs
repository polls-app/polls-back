using System.Security.Claims;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Api.Extractors;

public interface IUserExtractor
{
    Guid GetId();

    string GetEmail();
}

public sealed class UserExtractor(IHttpContextAccessor httpContextAccessor) : IUserExtractor
{
    public Guid GetId()
    {
        var userId = httpContextAccessor.HttpContext?.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value
            ?? throw new ApplicationException("No userId claim found");

        return new Guid(userId);
    }

    public string GetEmail()
    {
        var email = httpContextAccessor.HttpContext?.User.Claims.First(c => c.Type == ClaimTypes.Email).Value
            ?? throw new ApplicationException("No email claim found");

        return email;
    }
}