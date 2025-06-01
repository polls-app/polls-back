using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polls.Api.Authorization.Consts;
using Polls.Api.Contracts.Profile.Requests;
using Polls.Api.Contracts.Profile.Responses;
using Polls.Api.Extractors;
using Polls.Application.ProfileUseCases.Commands;
using Polls.Application.ProfileUseCases.Queries;

namespace Polls.Api.Controllers.v1.ProfileRestApi;

[ApiController]
[Route("api/v1/profiles")]
public class ProfileController(ISender mediator, IUserExtractor userExtractor) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy.EmailVerified)]
    public async Task<IActionResult> GetProfile()
    {
        var userId = userExtractor.GetId();

        var query = new GetProfileQuery(userId);
        var result = await mediator.Send(query);

        return Ok(new ProfileResponse(result.Username, result.Firstname, result.Lastname, result.Description, result.AvatarPath, result.ContributionCount));
    }

    [HttpPatch]
    [Authorize(Policy.EmailVerified)]
    public async Task<IActionResult> UpdateProfile(UpdateProfileRequest request)
    {
        var userId = userExtractor.GetId();

        var command = new UpdateProfileCommand(request.Username, request.Firstname, request.Lastname, request.Description, userId);
        await mediator.Send(command);

        return Ok();
    }

    [HttpPatch("avatar")]
    [Authorize(Policy.EmailVerified)]
    public async Task<IActionResult> ChangeAvatar(UpdateAvatarRequest request)
    {
        var userId = userExtractor.GetId();

        var command = new UpdateAvatarCommand(request.Path, userId);
        await mediator.Send(command);

        return Ok();
    }
}