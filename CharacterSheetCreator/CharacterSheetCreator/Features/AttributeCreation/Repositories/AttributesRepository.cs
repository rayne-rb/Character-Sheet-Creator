using System.Data;
using CharacterSheetCreator.Data.Services;
using CharacterSheetCreator.Features.AttributeCreation.DbModels;
using CharacterSheetCreator.Shared.Utilities;
using Npgsql;
using OneOf;
using RepoDb;

namespace CharacterSheetCreator.Features.AttributeCreation.Repositories;

public class AttributesRepository : IAttributesRepository
{
    private readonly IPgSqlDataSource _connectionFactory;

    public AttributesRepository(IPgSqlDataSource connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public NpgsqlConnection GetDbConnection()
    {
        var connection = _connectionFactory.GetDbConnection();
        if (connection.State != ConnectionState.Open)
            connection.Open();
        return connection;
    }
    public (NpgsqlConnection connection, NpgsqlTransaction transaction) BeginTransaction()
    {
        var connection = GetDbConnection();
        var transaction = connection.BeginTransaction();
        return (connection, transaction);
    }

    public async Task<OneOf<List<DbAttributeGroups>, AppError>> GetAttributeGroups()
    {
        await using var connection = _connectionFactory.GetDbConnection();
        try
        {
            var sql = """
                      SELECT * FROM attribute_groups
                      """;
            var results = await connection.ExecuteQueryAsync<DbAttributeGroups>(sql);
            if (results == null)
            {
                return new AppError();
            }
            return results.ToList();
        }
        catch (Exception e)
        {
            return new AppError($"Error getting attribute groups: {e.Message}");
        }
        
    }
}