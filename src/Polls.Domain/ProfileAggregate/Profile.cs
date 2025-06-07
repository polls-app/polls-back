using Polls.Domain.Base;
using Polls.Domain.ProfileAggregate.Events;
using Polls.Domain.ProfileAggregate.ValueObjects;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Domain.ProfileAggregate;

public sealed class Profile : AggregateRoot
{
    public Profile(
        ProfileId profileId,
        Username username,
        Fullname fullname,
        string description,
        Avatar avatar,
        uint followerCount,
        uint followingCount,
        uint postCount,
        uint contributionCount,
        UserId userId)
    {
        Id = profileId;
        Username = username;
        Fullname = fullname;
        Description = description;
        Avatar = avatar;
        FollowerCount = followerCount;
        FollowingCount = followingCount;
        PostCount = postCount;
        ContributionCount = contributionCount;
        UserId = userId;
    }

    public ProfileId Id { get; private set; }

    public Username Username { get; private set; }

    public Fullname Fullname { get; private set; }

    public string Description { get; private set; }

    public Avatar Avatar { get; private set; }

    public uint FollowerCount { get; private set; }

    public uint FollowingCount { get; private set; }

    public uint PostCount { get; private set; }

    public uint ContributionCount { get; private set; }

    public UserId UserId { get; private set; }

    public void Update(Username username, Fullname fullname, string description)
    {
        Username = username;
        Fullname = fullname;
        Description = description;
    }

    public void ChangeAvatar(Avatar avatar)
    {
        Avatar = avatar;
    }

    public void IncrementContribution() => ContributionCount++;

    public static Profile CreateDefault(UserId userId)
        => Create(
            userId,
            Username.Generate(),
            Fullname.Empty,
            description: string.Empty,
            Avatar.Default);

    private static Profile Create(
        UserId userId,
        Username username,
        Fullname fullname,
        string description,
        Avatar avatar)
    {
        var profile = new Profile(
            ProfileId.New(),
            username,
            fullname,
            description,
            avatar,
            followerCount: 0,
            followingCount: 0,
            postCount: 0,
            contributionCount: 0,
            userId);

        profile.AddEvent(new ProfileCreated(profile.Id, profile.Username, profile.UserId));
        return profile;
    }
}