using Dapper;
using Polls.Domain.ProfileAggregate;
using Polls.Domain.ProfileAggregate.Repositories;
using Polls.Domain.ProfileAggregate.ValueObjects;
using Polls.Domain.UserAggregate.ValueObjects;
using Polls.Infrastructure.Common.Persistence;
using Polls.Infrastructure.ProfileImplementations.Dtos;

namespace Polls.Infrastructure.ProfileImplementations.Repositories;

public class ProfileRepository(DapperContext context) : IProfileRepository
{
    private const string GetProfileByIdSql = """
                                             SELECT
                                                 p.id AS Id,
                                                 p.username AS Username,
                                                 p.first_name AS Firstname,
                                                 p.last_name AS Lastname,
                                                 p.description AS Description,
                                                 p.avatar_path AS AvatarPath,
                                                 p.contribution_count AS ContributionCount,
                                                 p.user_id AS UserId,
                                                 (SELECT COUNT(*) FROM followings f WHERE f.user_id = p.user_id) AS FollowerCount,
                                                 (SELECT COUNT(*) FROM followings f WHERE f.follower_id = p.user_id) AS FollowingCount,
                                                 (SELECT COUNT(*) FROM polls po WHERE po.user_id = p.user_id) AS PostCount
                                             FROM profiles p
                                             WHERE p.user_id = @UserId;
                                             """;

    private const string GetUsernameByIdSql = "SELECT username FROM profiles WHERE user_id = @UserId";

    private const string UpdateProfileSql = """
                                            UPDATE profiles
                                            SET
                                                id = @Id,
                                                username = @Username,
                                                first_name = @Firstname,
                                                last_name = @Lastname,
                                                description = @Description,
                                                avatar_path = @AvatarPath,
                                                contribution_count = @ContributionCount
                                            WHERE Id = @Id;
                                            """;

    private const string IsUsernameTakenSql = "SELECT COUNT(1) FROM profiles WHERE username = @Username";

    public async Task<Profile?> GetProfileByUserId(UserId userId)
    {
        await using var connection = context.CreateConnection();

        var profileMapping = await connection.QuerySingleOrDefaultAsync<ProfileMapping>(GetProfileByIdSql, new { UserId = userId.Value });

        return profileMapping is null
            ? null
            : new Profile(
                new ProfileId(profileMapping.Id),
                new Username(profileMapping.Username),
                new Fullname(new Name(profileMapping.Firstname), new Name(profileMapping.Lastname)),
                profileMapping.Description,
                new Avatar(profileMapping.AvatarPath),
                profileMapping.FollowerCount,
                profileMapping.FollowingCount,
                profileMapping.PostCount,
                profileMapping.ContributionCount,
                new UserId(profileMapping.UserId));
    }

    public async Task<Username> GetUsernameById(UserId userId)
    {
        await using var connection = context.CreateConnection();

        var usernameValue = await connection.QuerySingleOrDefaultAsync<string>(GetUsernameByIdSql, new { UserId = userId.Value });

        if (usernameValue is null)
            throw new InvalidOperationException($"No profile found for UserId: {userId.Value}");

        return new Username(usernameValue);
    }

    public async Task UpdateProfile(Profile profile)
    {
        await using var connection = context.CreateConnection();
        await connection.OpenAsync();

        await connection.ExecuteAsync(UpdateProfileSql, new
        {
            Id = profile.Id.Value,
            Username = profile.Username.Value,
            Firstname = profile.Fullname.Firstname.Value,
            Lastname = profile.Fullname.Lastname.Value,
            Description = profile.Description,
            AvatarPath = profile.Avatar.Path,
            ContributionCount = (int)profile.ContributionCount
        });
    }

    public async Task<bool> IsUsernameTaken(Username username)
    {
        await using var connection = context.CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(IsUsernameTakenSql, new { Username = username.Value });

        return count > 0;
    }
}