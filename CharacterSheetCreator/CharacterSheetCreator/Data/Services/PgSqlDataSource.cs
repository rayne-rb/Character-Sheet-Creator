using Npgsql;

namespace CharacterSheetCreator.Data.Services;

public class PgSqlDataSource : IPgSqlDataSource, IDisposable, IAsyncDisposable
{
    private readonly string _connectionString;
    private readonly NpgsqlDataSourceBuilder _dataSourceBuilder;
    private readonly NpgsqlDataSource _dataSource;

    public PgSqlDataSource(string connectionString)
    {
        _connectionString = connectionString;
        _dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        _dataSource = _dataSourceBuilder.Build();
    }

    public NpgsqlConnection GetDbConnection()
    {
        return _dataSource.OpenConnection();
    }

    public async Task<NpgsqlConnection> GetDbConnectionAsync()
    {
        return await _dataSource.OpenConnectionAsync();
    }

    public (NpgsqlConnection connection, NpgsqlTransaction transaction) BeginTransaction()
    {
        var connection = GetDbConnection();
        var transaction = connection.BeginTransaction();
        return (connection, transaction);
    }

    public void Dispose()
    {
        _dataSource.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _dataSource.DisposeAsync();
    }
}