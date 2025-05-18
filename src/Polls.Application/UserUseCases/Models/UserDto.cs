namespace Polls.Application.UserUseCases.Models;

public record UserDto(Guid Id, string Email, string Username, string Token);
