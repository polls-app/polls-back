using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polls.Api.Contracts.User.Requests;
using Polls.Api.Contracts.User.Responses;
using Polls.Api.Extractors;
using Polls.Application.UserUseCases.Authentication.Commands;
using Polls.Domain.UserAggregate.ValueObjects;
using Polls.Domain.Verification;

namespace Polls.Api.Controllers.v1.UserRestApi;

[ApiController]
[Route("api/v1/auth")]
public class AuthenticationController(ISender mediator, IUserExtractor userExtractor) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
    {
        var command = new RegisterUserCommand(request.Email, request.Password);

        var result = await mediator.Send(command);

        return Ok(new AuthResponse(result.Id, result.Email, result.Username, result.Token));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var command = new LoginUserCommand(new Email(request.Email), request.Password);

        var result = await mediator.Send(command);

        return Ok(new AuthResponse(result.Id, result.Email, result.Username, result.Token));
    }

    [Authorize]
    [HttpPatch("confirm-email")]
    public async Task<ActionResult<AuthResponse>> ConfirmEmail(ConfirmEmailRequest request)
    {
        var email = userExtractor.GetEmail();

        var command = new VerifyEmailCommand(new VerificationToken(request.Token), email);
        var result = await mediator.Send(command);

        return Ok(new AuthResponse(result.Id, result.Email, result.Username, result.Token));
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

    [HttpPost("password-reset-request")]
    public async Task<IActionResult> SendChangePasswordEmail(string email)
    {
        var command = new SendChangePasswordEmailCommand(new Email(email));
        await mediator.Send(command);

        return Ok();
    }

    [HttpPatch("password-reset")]
    public async Task<IActionResult> ChangePassword(string token, ChangePasswordRequest request)
    {
        var command = new ChangePasswordCommand(
            new Email(request.Email),
            Password.CreateHashed(request.NewPassword),
            new VerificationToken(token));

        await mediator.Send(command);

        return Ok();
    }
}
