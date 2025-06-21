using CharacterSheetCreator.Features.AttributeCreation.Models;
using CharacterSheetCreator.Shared.Utilities;
using OneOf;

namespace CharacterSheetCreator.Features.AttributeCreation.Services;

public interface IAttributesService
{
    Task<OneOf<List<AttributeGroupsDto>, AppError>> GetAttributeGroups();
    Task<OneOf<bool, AppError>> CreateAttributeGroup(string groupName);
    Task<OneOf<List<AttributeDto>, AppError>> GetAttributes(int groupId);
}