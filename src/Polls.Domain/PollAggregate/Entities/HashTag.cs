using Polls.Domain.PollAggregate.ValueObjects;

namespace Polls.Domain.PollAggregate.Entities;

public sealed class HashTag(string name)
{
    public HashTagId Id { get; private set; } = HashTagId.New();

    public string Name { get; private set; } = name;
}