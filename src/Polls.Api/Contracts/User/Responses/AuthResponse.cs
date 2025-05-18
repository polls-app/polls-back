namespace Polls.Api.Contracts.User.Responses;

public record AuthResponse(Guid Id, string Email, string Username, string Token);
