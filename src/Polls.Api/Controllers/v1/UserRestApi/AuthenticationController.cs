using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polls.Api.Contracts.User.Requests;
using Polls.Api.Contracts.User.Responses;
using Polls.Api.Extractors;
using Polls.Application.UserUseCases.Authentication.Commands;

namespace Polls.Api.Controllers.v1.UserRestApi;

[ApiController]
[Route("api/v1/auth")]
public class AuthenticationController(ISender mediator, IUserExtractor userExtractor) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterUserCommand(request.Email, request.Password);

        var result = await mediator.Send(command);

        return Ok(new AuthResponse(result.Id, result.Email, result.Username, result.Token));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = new LoginUserCommand(request.Email, request.Password);

        var result = await mediator.Send(command);

        return Ok(new AuthResponse(result.Id, result.Email, result.Username, result.Token));
    }

    [Authorize]
    [HttpPatch("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequest request)
    {
        var email = userExtractor.GetEmail();

        var command = new VerifyEmailCommand(request.Token, email);
        await mediator.Send(command);

        return Ok();
    }

    [Authorize]
    [HttpPost("resend-token")]
    public async Task<IActionResult> ResendToken()
    {
        var email = userExtractor.GetEmail();

        var command = new ResendTokenCommand(email);
        await mediator.Send(command);

        return Ok();
    }
}
