using Polls.Domain.PollAggregate.ValueObjects;

namespace Polls.Domain.PollAggregate.Entities;

public sealed class Category(string name)
{
    public CategoryId Id { get; private set; } = CategoryId.New();

    public string Name { get; private set; } = name;
}