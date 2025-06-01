namespace Polls.Api.Contracts.Profile.Requests;

public sealed record UpdateProfileRequest(
    string Username,
    string Firstname,
    string Lastname,
    string Description
);