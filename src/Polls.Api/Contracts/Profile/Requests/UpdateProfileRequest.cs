namespace Polls.Api.Contracts.Profile.Requests;

public sealed record UpdateProfileRequest(
    string? Username = null,
    string? Firstname = null,
    string? Lastname = null,
    string? Description = null
);