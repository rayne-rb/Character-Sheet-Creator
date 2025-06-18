using CharacterSheetCreator.Data.Services;
using CharacterSheetCreator.Features.AttributeCreation.Models;
using CharacterSheetCreator.Features.AttributeCreation.Repositories;
using CharacterSheetCreator.Shared.Utilities;
using OneOf;

namespace CharacterSheetCreator.Features.AttributeCreation.Services;

public class AttributesService(IAttributesRepository attributesRepository) : IAttributesService
{
    private IAttributesRepository _attributesRepository = attributesRepository;
    private readonly IPgSqlDataSource _connectionFactory;
    
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
        var (connection, transaction) = _connectionFactory.BeginTransaction();
        try
        {
            var result = await _attributesRepository.CreateAttributeGroup(groupName, connection, transaction);
            if (result.IsT1)
            {
                return new AppError(result.AsT1.ErrorMessage);
            }

            return result.AsT0;
        }
        catch (Exception e)
        {
            return new AppError(e.Message);
        }
    }
}