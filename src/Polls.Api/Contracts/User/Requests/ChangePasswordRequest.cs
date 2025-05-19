namespace Polls.Api.Contracts.User.Requests;

public record ChangePasswordRequest(
    string Email,
    string NewPassword
);