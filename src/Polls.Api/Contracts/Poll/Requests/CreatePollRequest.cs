namespace Polls.Api.Contracts.Poll.Requests;

public sealed record CreatePollRequest(
    string Title,
    string Description,
    string Color,
    string Language,
    string AccessMode,
    Guid CategoryId,
    IList<Guid> HashtagIds,
    IList<Guid> OptionIds
);