using MediatR;
using Polls.Domain.PollAggregate;
using Polls.Domain.PollAggregate.Entities;
using Polls.Domain.PollAggregate.Repositories;
using Polls.Domain.PollAggregate.ValueObjects;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Application.PollUseCases.Commands;

public sealed record CreatePollCommand(
    Title Title,
    string Description,
    Color Color,
    Language Language,
    string AccessMode,
    CategoryId CategoryId,
    IList<HashTagId> HashtagIds,
    IList<OptionId> OptionIds,
    UserId UserId
) : IRequest;

public sealed class CreatePollCommandHandler(
    ICategoryRepository categoryRepository,
    IHashtagRepository hashtagRepository,
    IOptionRepository optionRepository,
    IPollRepository pollRepository
) : IRequestHandler<CreatePollCommand>
{
    public async Task Handle(CreatePollCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetById(request.CategoryId);
        if (category is null)
            throw new ApplicationException("Category not found");

        var hashTags = await GetHashtags(request.HashtagIds);
        var options = await GetOptions(request.OptionIds);

        var poll = Poll.Create(request.Title, request.Description, request.Color, request.Language, request.AccessMode, category, request.UserId);
        poll.AddHashTags(hashTags);
        poll.AddOptions(options);

        await pollRepository.Add(poll);
    }

    private async Task<IEnumerable<HashTag>> GetHashtags(IEnumerable<HashTagId> hashtagIds)
    {
        var hashtags = new List<HashTag>();

        foreach (var hashtagId in hashtagIds)
        {
            var hashtag = await hashtagRepository.GetById(hashtagId)
                          ?? throw new ApplicationException("Hashtag not found");

            hashtags.Add(hashtag);
        }

        return hashtags;
    }

    private async Task<IEnumerable<Option>> GetOptions(IEnumerable<OptionId> optionIds)
    {
        var options = new List<Option>();

        foreach (var optionId in optionIds)
        {
            var option = await optionRepository.GetById(optionId)
                          ?? throw new ApplicationException("Hashtag not found");

            options.Add(option);
        }

        return options;
    }
}