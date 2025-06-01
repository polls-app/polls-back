using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polls.Api.Authorization.Consts;
using Polls.Api.Contracts.Poll.Requests;
using Polls.Api.Extractors;
using Polls.Application.PollUseCases.Commands;
using Polls.Domain.PollAggregate.ValueObjects;

namespace Polls.Api.Controllers.v1.PollRestApi;

[ApiController]
[Route("api/v1/polls")]
public class PollController(ISender mediator, IUserExtractor userExtractor) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy.EmailVerified)]
    public async Task<IActionResult> CreatePoll(CreatePollRequest request)
    {
        var userId = userExtractor.GetId();

        var command = new CreatePollCommand(
            new Title(request.Title),
            request.Description,
            new Color(request.Color),
            new Language(request.Language),
            request.AccessMode,
            new CategoryId(request.CategoryId),
            request.HashtagIds.Select(x => new HashTagId(x)).ToList(),
            request.OptionIds.Select(x => new OptionId(x)).ToList(),
            userId);

        await mediator.Send(command);
        return Created();
    }
}