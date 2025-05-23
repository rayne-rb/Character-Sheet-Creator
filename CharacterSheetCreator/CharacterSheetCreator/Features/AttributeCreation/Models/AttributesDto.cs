namespace CharacterSheetCreator.Features.AttributeCreation.Models;

public class AttributeGroupsDto
{
    public int AttributeGroupId { get; set; }
    public string? AttributeGroupName { get; set; } = string.Empty;
    public List<AttributeDto?> Attributes { get; set; } = new List<AttributeDto?>();
    
}

public class AttributeDto
{
    public int AttributeId { get; set; }
    public string? AttributeName { get; set; } = string.Empty;
    public int AttributeGroupId { get; set; }
    
}