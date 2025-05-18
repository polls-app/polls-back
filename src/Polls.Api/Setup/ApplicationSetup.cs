namespace Polls.Api.Setup;

public static class ApplicationSetup
{
    public static IServiceCollection AddApplicationSetup(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}