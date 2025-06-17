using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterSheetCreator.Features.AttributeCreation.DbModels;

[Table("attributes")]
public class DbAttribute
{
    public int id { get; set; }
    public string attribute_name { get; set; } = "";
    public int attribute_group_id { get; set; }
    public int attribute_type { get; set; }
}