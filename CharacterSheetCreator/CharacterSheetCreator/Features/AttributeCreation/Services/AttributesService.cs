using CharacterSheetCreator.Data.Services;
using CharacterSheetCreator.Features.AttributeCreation.Models;
using CharacterSheetCreator.Features.AttributeCreation.Repositories;
using CharacterSheetCreator.Shared.Utilities;
using OneOf;

namespace CharacterSheetCreator.Features.AttributeCreation.Services;

public class AttributesService : IAttributesService
{
    private IAttributesRepository _attributesRepository;
    private readonly IPgSqlDataSource _connectionFactory;

    public AttributesService(IAttributesRepository attributesRepository, IPgSqlDataSource connectionFactory)
    {
        _attributesRepository = attributesRepository;
        _connectionFactory = connectionFactory;
    }

    public async Task<OneOf<List<AttributeGroupsDto>, AppError>> GetAllAttributeGroups()
    {
        try
        {
            var results = await _attributesRepository.GetAttributeGroups();
            if (results.IsT1)
            {
                return new AppError(results.AsT1.ErrorMessage);
            }

            var attributesResult = await GetAllAttributes();
            if (attributesResult.IsT1)
            {
                return new AppError(attributesResult.AsT1.ErrorMessage);
            }
            
            var mappedResults = new List<AttributeGroupsDto>();
            foreach (var result in results.AsT0)
            {
                var mappedResult = new AttributeGroupsDto()
                {
                    Id = result.id,
                    AttributeGroupName = result.group_name
                };
                foreach (var attribute in attributesResult.AsT0.Where(x => x.AttributeGroupId == mappedResult.Id))
                {
                    mappedResult.Attributes.Add(attribute);
                }
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

    public async Task<OneOf<List<AttributeDto>, AppError>> GetAttributes(int groupId)
    {
        try
        {
            var results = await _attributesRepository.GetAttributesByGroupId(groupId);
            if (results.IsT1)
            {
                return new AppError(results.AsT1.ErrorMessage);
            }

            var mappedResults = new List<AttributeDto>();
            foreach (var result in results.AsT0)
            {
                var mappedResult = new AttributeDto()
                {
                    AttributeId = result.id,
                    AttributeName = result.attribute_name,
                    AttributeGroupId = result.attribute_group_id,
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

    public async Task<OneOf<bool, AppError>> CreateAttribute(int groupId, string attributeName)
    {
        try
        {
            var result = await _attributesRepository.CreateAttribute(groupId, attributeName);
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

    public async Task<OneOf<List<AttributeDto>, AppError>> GetAllAttributes()
    {
        try
        {
            var results = await _attributesRepository.GetAllAttributes();
            if (results.IsT1)
            {
                return new AppError(results.AsT1.ErrorMessage);
            }

            var mappedResults = new List<AttributeDto>();
            foreach (var result in results.AsT0)
            {
                var mappedResult = new AttributeDto()
                {
                    AttributeId = result.id,
                    AttributeName = result.attribute_name,
                    AttributeGroupId = result.attribute_group_id,
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
}