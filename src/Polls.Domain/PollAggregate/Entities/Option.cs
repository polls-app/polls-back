using Polls.Domain.PollAggregate.ValueObjects;

namespace Polls.Domain.PollAggregate.Entities;

public sealed class Option
{
    public Option(string content, uint position, PollId pollId, bool? isCorrect = null)
    {
        Id = OptionId.New();
        Content = content;
        Position = position;
        IsCorrect = isCorrect;
        PollId = pollId;
    }

    public OptionId Id { get; private set; }

    public string Content { get; private set; }

    public uint Position { get; private set; }

    public bool? IsCorrect { get; private set; }

    public PollId PollId { get; private set; }
}