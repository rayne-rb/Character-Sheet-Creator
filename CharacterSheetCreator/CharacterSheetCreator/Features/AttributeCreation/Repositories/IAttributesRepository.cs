using System.Data;
using CharacterSheetCreator.Features.AttributeCreation.DbModels;
using CharacterSheetCreator.Shared.Utilities;
using OneOf;

namespace CharacterSheetCreator.Features.AttributeCreation.Repositories;

public interface IAttributesRepository
{
    Task<OneOf<List<DbAttributeGroups>, AppError>> GetAttributeGroups(IDbConnection connection, IDbTransaction transaction); 
    
}