using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace Polls.Api.Authorization.Requirements;

public class EmailVerifiedRequirement : IAuthorizationRequirement;

public class EmailVerifiedHandler : AuthorizationHandler<EmailVerifiedRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailVerifiedRequirement requirement)
    {
        const string trueValue = "true";

        var emailVerified = context.User.FindFirst(JwtRegisteredClaimNames.EmailVerified)?.Value;

        if (emailVerified is trueValue)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
