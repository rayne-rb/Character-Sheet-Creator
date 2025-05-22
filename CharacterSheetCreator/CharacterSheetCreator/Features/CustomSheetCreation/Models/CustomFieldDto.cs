using CharacterSheetCreator.Shared.Enums;

namespace CharacterSheetCreator.Features.CustomSheetCreation.Models;

public class CustomFieldDto
{
    public CustomFieldType FieldType { get; set; }
    public string FieldName { get; set; } = string.Empty;
    public bool? HasLabel { get; set; }
    public string? LabelName {get; set;} = string.Empty;
    public object? Value { get; set; }
}