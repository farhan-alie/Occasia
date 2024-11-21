using System.Data.Common;
using Npgsql;
using Occasia.Common.Application.Data;

namespace Occasia.Common.Infastructure.Data;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync().ConfigureAwait(false);
    }
}
