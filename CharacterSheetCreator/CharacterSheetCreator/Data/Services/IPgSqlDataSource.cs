using Npgsql;

namespace CharacterSheetCreator.Data.Services;

public interface IPgSqlDataSource
{
    NpgsqlConnection GetDbConnection();
    Task<NpgsqlConnection> GetDbConnectionAsync();
}
