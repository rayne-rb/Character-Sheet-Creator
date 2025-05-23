using CharacterSheetCreator.Data.Services;
using CharacterSheetCreator.Features.AttributeCreation.DbModels;
using CharacterSheetCreator.Shared.Utilities;
using OneOf;

namespace CharacterSheetCreator.Features.AttributeCreation.Repositories;

public class AttributesRepository : IAttributesRepository
{
    private readonly IDatabaseManager _databaseManager;
    public async Task<OneOf<List<DbAttributeGroups>, AppError>> GetAttributeGroups()
    {
        throw new NotImplementedException();
    }
}