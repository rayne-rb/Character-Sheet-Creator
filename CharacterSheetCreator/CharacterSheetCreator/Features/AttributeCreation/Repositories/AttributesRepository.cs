using System.Data;
using CharacterSheetCreator.Features.AttributeCreation.DbModels;
using CharacterSheetCreator.Shared.Utilities;
using OneOf;

namespace CharacterSheetCreator.Features.AttributeCreation.Repositories;

public class AttributesRepository : IAttributesRepository
{
    public async Task<OneOf<List<DbAttributeGroups>, AppError>> GetAttributeGroups(IDbConnection connection, IDbTransaction transaction)
    {
        throw new NotImplementedException();
    }
}