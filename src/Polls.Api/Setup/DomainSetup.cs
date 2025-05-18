using Polls.Domain.FollowerAggregate.Services;
using Polls.Domain.UserAggregate.Services;

namespace Polls.Api.Setup;

public static class DomainSetup
{
    public static IServiceCollection AddDomainSetup(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<FollowUserService>();
        services.AddScoped<RegisterUserService>();

        return services;
    }
}