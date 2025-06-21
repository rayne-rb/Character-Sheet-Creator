using System.Data;
using CharacterSheetCreator.Features.AttributeCreation.DbModels;
using CharacterSheetCreator.Shared.Utilities;
using OneOf;

namespace CharacterSheetCreator.Features.AttributeCreation.Repositories;

public interface IAttributesRepository
{
    Task<OneOf<List<DbAttributeGroup>, AppError>> GetAttributeGroups(); 
    Task<OneOf<bool, AppError>> CreateAttributeGroup(string groupName, IDbConnection? connection = null, IDbTransaction? transaction = null);
    Task<OneOf<List<DbAttribute>, AppError>> GetAttributesByGroupId(int groupId); 
    Task<OneOf<bool, AppError>> CreateAttribute(int groupId, string attributeName, IDbConnection? connection = null, IDbTransaction? transaction = null);
    Task<OneOf<List<DbAttribute>, AppError>> GetAllAttributes(); 
}