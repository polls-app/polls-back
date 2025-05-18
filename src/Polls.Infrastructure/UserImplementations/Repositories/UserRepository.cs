using System.Data;
using Dapper;
using NpgsqlTypes;
using Polls.Application.UserUseCases.Models;
using Polls.Domain.ProfileAggregate;
using Polls.Domain.ProfileAggregate.ValueObjects;
using Polls.Domain.UserAggregate;
using Polls.Domain.UserAggregate.Repositories;
using Polls.Domain.UserAggregate.ValueObjects;
using Polls.Infrastructure.Common.Persistence;
using Polls.Infrastructure.UserImplementations.Repositories.Dtos;

namespace Polls.Infrastructure.UserImplementations.Repositories;

public class UserRepository(DapperContext context) : IUserRepository
{
    private const string GetByEmailSql = """
                                         SELECT
                                             id,
                                             email,
                                             password,
                                             role,
                                             is_verified AS IsVerified,
                                             last_login AS LastLoginAt,
                                             joined_at AS JoinedAt
                                         FROM users WHERE email = @Email
                                         """;

    private const string IsEmailTakenSql = "SELECT COUNT(1) FROM users WHERE email = @Email";

    private const string IsUsernameTakenSql = "SELECT COUNT(1) FROM profiles WHERE username = @Username";

    private const string IsUserExistsSql = "SELECT COUNT(1) FROM users WHERE id = @Id";

    private const string InsertUserSql = """
                                         INSERT INTO users (id, email, password, role, is_verified, last_login, joined_at)
                                         VALUES (@Id, @Email, @Password, @Role, @IsVerified, @LastLogin, @JoinedAt);
                                         """;

    private const string InsertProfileSql = """
                                            INSERT INTO profiles (id, username, first_name, last_name, description, avatar_path, contribution_count, user_id)
                                            VALUES (@Id, @Username, @Firstname, @Lastname, @Description, @AvatarUrl, @ContributionCount, @UserId);
                                            """;

    private const string UpdateUserSql = """
                                            UPDATE users
                                            SET
                                                id = @Id,
                                                email = @Email,
                                                password = @Password,
                                                role = @Role,
                                                is_verified = @IsVerified,
                                                last_login = @LastLogin,
                                                joined_at = @JoinedAt
                                            WHERE Id = @Id;
                                         """;

    public async Task<User?> GetByEmailAsync(Email email)
    {
        await using var connection = context.CreateConnection();
        var userDto = await connection.QueryFirstOrDefaultAsync<UserMapping>(GetByEmailSql, new { Email = email.Value });

        return userDto is null
            ? null
            : new User(
                new UserId(userDto.Id),
                new Email(userDto.Email, userDto.IsVerified),
                new Password(userDto.Password),
                userDto.Role,
                userDto.LastLoginAt,
                userDto.JoinedAt);
    }

    public async Task<bool> IsEmailTakenAsync(Email email)
    {
        await using var connection = context.CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(IsEmailTakenSql, new { Email = email.Value });

        return count > 0;
    }

    public async Task<bool> IsUsernameTakenAsync(Username username)
    {
        await using var connection = context.CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(IsUsernameTakenSql, new { Username = username.Value });

        return count > 0;
    }

    public async Task<bool> ExistsAsync(UserId userId)
    {
        await using var connection = context.CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(IsUserExistsSql, new { Id = userId.Value });

        return count > 0;
    }

    public async Task AddAsync(User user, Profile profile)
    {
        await using var connection = context.CreateConnection();
        await connection.OpenAsync();

        await using var transaction = await connection.BeginTransactionAsync();
        try
        {
            await InsertUserAsync(user, connection, transaction);
            await InsertProfileAsync(profile, connection, transaction);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateAsync(User user)
    {
        await using var connection = context.CreateConnection();
        await connection.OpenAsync();

        await connection.ExecuteAsync(UpdateUserSql, new
        {
            Id = user.Id.Value,
            Email = user.Email.Value,
            Password = user.Password.Hashed,
            Role = user.Role.ToString(),
            IsVerified = user.Email.IsVerified,
            LastLogin = user.LastLoginAt,
            JoinedAt = user.JoinedAt
        });
    }

    private static async Task InsertUserAsync(User user, IDbConnection connection, IDbTransaction transaction)
        => await connection.ExecuteAsync(InsertUserSql, new
        {
            Id = user.Id.Value,
            Email = user.Email.Value,
            Password = user.Password.Hashed,
            Role = user.Role.ToString(),
            IsVerified = user.Email.IsVerified,
            LastLogin = user.LastLoginAt,
            JoinedAt = user.JoinedAt
        }, transaction);

    private static async Task InsertProfileAsync(Profile profile, IDbConnection connection, IDbTransaction transaction)
        => await connection.ExecuteAsync(InsertProfileSql, new
        {
            Id = profile.Id.Value,
            Username = profile.Username.Value,
            Firstname = profile.Fullname.Firstname.Value,
            Lastname = profile.Fullname.Lastname.Value,
            Description = profile.Description,
            AvatarUrl = profile.Avatar.Path,
            ContributionCount = (int)profile.ContributionCount,
            UserId = profile.UserId.Value
        }, transaction);
}