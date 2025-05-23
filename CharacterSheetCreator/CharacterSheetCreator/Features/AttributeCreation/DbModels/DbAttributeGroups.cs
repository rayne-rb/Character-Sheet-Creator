using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterSheetCreator.Features.AttributeCreation.DbModels;

[Table("attribute_groups")]
public class DbAttributeGroups
{
    public int id { get; set; }
    public string group_name { get; set; }
}