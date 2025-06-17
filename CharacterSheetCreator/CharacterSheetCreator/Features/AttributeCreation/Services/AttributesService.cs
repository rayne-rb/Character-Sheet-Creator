using CharacterSheetCreator.Features.AttributeCreation.Models;
using CharacterSheetCreator.Features.AttributeCreation.Repositories;
using CharacterSheetCreator.Shared.Utilities;
using OneOf;

namespace CharacterSheetCreator.Features.AttributeCreation.Services;

public class AttributesService(IAttributesRepository attributesRepository) : IAttributesService
{
    private IAttributesRepository _attributesRepository = attributesRepository;

    public async Task<OneOf<List<AttributeGroupsDto>, AppError>> GetAttributeGroups()
    {
        try
        {
            var results = await _attributesRepository.GetAttributeGroups();
            if (results.IsT1)
            {
                return new AppError(results.AsT1.ErrorMessage);
            }
            var mappedResults = new List<AttributeGroupsDto>();
            foreach (var result in results.AsT0)
            {
                var mappedResult = new AttributeGroupsDto()
                {
                    AttributeGroupId = result.id,
                    AttributeGroupName = result.group_name
                };
                mappedResults.Add(mappedResult);
            }
            return mappedResults;
        }
        catch (Exception e)
        {
            return new AppError(e.Message);
        }
    }

    public async Task<OneOf<bool, AppError>> CreateAttributeGroup(string groupName)
    {
        try
        {
            var result = await _attributesRepository.CreateAttributeGroup(groupName);
        }
        catch (Exception e)
        {
            return new AppError(e.Message);
        }
    }
}