using CharacterSheetCreator.Features.AttributeCreation.Models;
using CharacterSheetCreator.Shared.Utilities;
using OneOf;

namespace CharacterSheetCreator.Features.AttributeCreation.Services;

public interface IAttributesService
{
    Task<OneOf<List<AttributeGroupsDto>, AppError>> GetAllAttributeGroups();
    Task<OneOf<bool, AppError>> CreateAttributeGroup(string groupName);
    Task<OneOf<List<AttributeDto>, AppError>> GetAttributes(int groupId);
    Task<OneOf<bool, AppError>> CreateAttribute(int groupId, string attributeName);
    Task<OneOf<List<AttributeDto>, AppError>> GetAllAttributes();
}