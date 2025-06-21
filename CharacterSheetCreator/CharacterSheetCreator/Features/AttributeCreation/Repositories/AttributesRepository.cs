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

    public async Task<OneOf<bool, AppError>> CreateAttributeGroup(string groupName, IDbConnection? connection = null,
        IDbTransaction? transaction = null)
    {
        var connectionPassed = true;
        var error = true;
        if (connection == null)
        {
            (connection, transaction) = _connectionFactory.BeginTransaction();
            connectionPassed = false;
        }

        try
        {
            var _id = 0;
            var sql = """
                      INSERT INTO public.attribute_groups (id, group_name)
                      VALUES (@group_id, @name);
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

            var parameters = new {group_id = _id, name = groupName};

            await connection.ExecuteQueryAsync(sql, parameters);

            error = false;
            return true;
        }
        catch (Exception e)
        {
            return new AppError($"Error creating attribute group: {e.Message}");
        }
        finally
        {
            if (!connectionPassed)
            {
                if (!error)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
                transaction.Dispose();
                connection.Dispose();
            }
        }
    }

    public async Task<OneOf<List<DbAttribute>, AppError>> GetAttributesByGroupId(int groupId)
    {
        await using var connection = _connectionFactory.GetDbConnection();
        try
        {
            var sql = """
                      SELECT * FROM attributes WHERE attribute_group_id = @group_id
                      """;
            var parameters = new {group_id = groupId};
            var results = await connection.ExecuteQueryAsync<DbAttribute>(sql, parameters);
            if (results == null)
            {
                return new List<DbAttribute>();
            }
            return results.ToList();
        }
        catch (Exception e)
        {
            return new AppError($"Error getting attribute groups: {e.Message}");
        }
    }

    public async Task<OneOf<bool, AppError>> CreateAttribute(int groupId, string attributeName,
        IDbConnection? connection = null, IDbTransaction? transaction = null)
    {
        var connectionPassed = true;
        var error = true;
        if (connection == null)
        {
            (connection, transaction) = _connectionFactory.BeginTransaction();
            connectionPassed = false;
        }

        try
        {
            var _id = 0;
            var sql = """
                      INSERT INTO public.attributes (id, attribute_name, attribute_group_id)
                      VALUES (@attribute_id, @name, @group_id);
                      """;
            var existingAttributesResult = await GetAllAttributes();
            if (existingAttributesResult.IsT1)
            {
                return new AppError("Error Getting existing attributes");
            }

            if (existingAttributesResult.AsT0.Count <= 0)
            {
                _id = 1;
            }
            else
            {
                _id = existingAttributesResult.AsT0.Max(x => x.id) + 1;
            }

            var parameters = new {attribute_id = _id, name = attributeName, group_id = groupId};

            await connection.ExecuteQueryAsync(sql, parameters);

            error = false;
            return true;
        }
        catch (Exception e)
        {
            return new AppError($"Error creating attribute: {e.Message}");
        }
        finally
        {
            if (!connectionPassed)
            {
                if (!error)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
                transaction.Dispose();
                connection.Dispose();
            }
        }
    }

    public async Task<OneOf<List<DbAttribute>, AppError>> GetAllAttributes()
    {
        await using var connection = _connectionFactory.GetDbConnection();
        try
        {
            var sql = """
                      SELECT * FROM attributes
                      """;
            var results = await connection.ExecuteQueryAsync<DbAttribute>(sql);
            if (results == null)
            {
                return new List<DbAttribute>();
            }
            return results.ToList();
        }
        catch (Exception e)
        {
            return new AppError($"Error getting attribute groups: {e.Message}");
        }
    }
}