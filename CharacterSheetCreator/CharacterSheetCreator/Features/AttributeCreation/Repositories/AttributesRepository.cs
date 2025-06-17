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

    public async Task<OneOf<List<DbAttributeGroup>, AppError>> GetAttributeGroups()
    {
        await using var connection = _connectionFactory.GetDbConnection();
        try
        {
            var sql = """
                      SELECT * FROM attribute_groups
                      """;
            var results = await connection.ExecuteQueryAsync<DbAttributeGroup>(sql);
            if (results == null)
            {
                return new List<DbAttributeGroup>();
            }
            return results.ToList();
        }
        catch (Exception e)
        {
            return new AppError($"Error getting attribute groups: {e.Message}");
        }
        
    }

    public async Task<OneOf<bool, AppError>> CreateAttributeGroup(string groupName, IDbConnection connection,
        IDbTransaction transaction = null)
    {
        var connectionPassed = true;
        if (connection == null)
        {
            (connection, transaction) = _connectionFactory.();
            connectionPassed = false;
        }
        try
        {
            var _id = 0;
            var sql = """
                      INSERT INTO public.attribute_groups (id, group_name)
                      VALUES (@id, @group_name);
                      """;
            var existingGroupResult = await GetAttributeGroups();
            if (existingGroupResult.IsT1)
            {
                return new AppError("Error Getting existing attribute groups");
            }

            if (existingGroupResult.AsT0.Count <= 0)
            {
                _id = 1;
            }
            else
            {
                _id = existingGroupResult.AsT0.Max(x => x.id) + 1;
            }
            var parameters = new {id = _id, group_name = groupName};
            
            var rowsAffected = await connection.ExecuteNonQueryAsync(sql, parameters);
            if (rowsAffected == 0 || rowsAffected > 1)
            {
                return false;
            }

            return true;
        }
        catch (Exception e)
        {
            return new AppError($"Error creating attribute group: {e.Message}");
        }
    }
}