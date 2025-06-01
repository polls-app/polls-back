using Polls.Domain.PollAggregate.Entities;
using Polls.Domain.PollAggregate.Repositories;
using Polls.Domain.PollAggregate.ValueObjects;

namespace Polls.Infrastructure.PollImplementations.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public Task<Category?> GetById(CategoryId categoryId)
    {
        throw new NotImplementedException();
    }
}