using Polls.Application.Abstractions.Authentication;
using Polls.Application.Abstractions.Senders;
using Polls.Domain.Base.Events;
using Polls.Domain.FollowerAggregate.Repositories;
using Polls.Domain.PollAggregate.Repositories;
using Polls.Domain.ProfileAggregate.Repositories;
using Polls.Domain.UserAggregate.Repositories;
using Polls.Domain.Verification.Repositories;
using Polls.Infrastructure.Common.Events;
using Polls.Infrastructure.Common.Persistence;
using Polls.Infrastructure.Common.Persistence.Configurations;
using Polls.Infrastructure.FollowerImplementations.Repositories;
using Polls.Infrastructure.PollImplementations.Repositories;
using Polls.Infrastructure.ProfileImplementations.Repositories;
using Polls.Infrastructure.Senders;
using Polls.Infrastructure.Senders.EmailSenders;
using Polls.Infrastructure.Senders.EmailSenders.Configurations;
using Polls.Infrastructure.UserImplementations.Authentication;
using Polls.Infrastructure.UserImplementations.Authentication.Configurations;
using Polls.Infrastructure.UserImplementations.Repositories;
using Polls.Infrastructure.VerificationImplementations.Repositories;

namespace Polls.Api.Setup;

public static class InfrastructureSetup
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCommon(configuration);
        services.AddSenders(configuration);
        services.AddFollowerImplementations(configuration);
        services.AddPollImplementations(configuration);
        services.AddProfileImplementations(configuration);
        services.AddUserImplementations(configuration);
        services.AddVerificationImplementations(configuration);

        return services;
    }

    private static void AddCommon(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
        services.Configure<DbSettings>(configuration.GetSection(nameof(DbSettings)));
        services.AddSingleton<DapperContext>();
    }

    private static void AddSenders(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmtpSettings>(configuration.GetSection(nameof(SmtpSettings)));
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IVerificationTokenSender, VerificationTokenSender>();
    }

    private static void AddFollowerImplementations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IFollowerRepository, FollowerRepository>();
    }

    private static void AddPollImplementations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IHashtagRepository, HashtagRepository>();
        services.AddScoped<IOptionRepository, OptionRepository>();
        services.AddScoped<IPollRepository, PollRepository>();
    }

    private static void AddProfileImplementations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProfileRepository, ProfileRepository>();
    }

    private static void AddUserImplementations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
    }

    private static void AddVerificationImplementations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.AddScoped<IVerificationTokenRepository, VerificationTokenRepository>();
    }
}