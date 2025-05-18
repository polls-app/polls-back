using System.Data.Common;
using Microsoft.Extensions.Options;
using Npgsql;
using Polls.Infrastructure.Common.Persistence.Configurations;

namespace Polls.Infrastructure.Common.Persistence;

public class DapperContext(IOptions<DbSettings> dbOptions)
{
    private readonly DbSettings _dbSettings = dbOptions.Value;

    public DbConnection CreateConnection() => new NpgsqlConnection(_dbSettings.ConnectionString);
}
