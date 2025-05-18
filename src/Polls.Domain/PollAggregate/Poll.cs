using Polls.Domain.Base;
using Polls.Domain.PollAggregate.Entities;
using Polls.Domain.PollAggregate.ValueObjects;
using Polls.Domain.UserAggregate.ValueObjects;

namespace Polls.Domain.PollAggregate;

public sealed class Poll : AggregateRoot
{
    private readonly List<HashTag> _hashTags = new();
    private readonly List<Option> _options = new();

    private Poll(PollId id, Title title, string description, Color color, Language language, string accessMode, Category category, UserId userId)
    {
        Id = id;
        Title = title;
        Description = description;
        Color = color;
        Language = language;
        AccessMode = accessMode;
        Category = category;
        UserId = userId;
    }

    public PollId Id { get; private set; }

    public Title Title { get; private set; }

    public string? Description { get; private set; }

    public int PossibleAnswersCount { get; private set; }

    public Color Color { get; private set; }

    public Language Language { get; private set; }

    public string AccessMode { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Category Category { get; private set; }

    public IReadOnlyList<HashTag> HashTags => _hashTags.AsReadOnly();

    public IReadOnlyList<Option> Options => _options.AsReadOnly();

    public UserId UserId { get; private set; }

    public void AddHashTag(HashTag hashTag) => _hashTags.Add(hashTag);

    public void AddHashTags(IEnumerable<HashTag> hashTags) => _hashTags.AddRange(hashTags);

    public void AddOption(Option option) => _options.Add(option);

    public void AddOptions(IEnumerable<Option> options) => _options.AddRange(options);

    public static Poll Create(Title title, string description, Color color, Language language, string accessMode, Category category, UserId userId)
    {
        return new Poll(PollId.New(), title, description, color, language, accessMode, category, userId);
    }
}