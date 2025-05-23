using CharacterSheetCreator.Shared.Enums;

namespace CharacterSheetCreator.Features.CustomSheetCreation.Models;

public class CustomSheetDto
{
    public int SheetId { get; set; }
    public string SheetName { get; set; } = string.Empty;
    public List<CustomFieldDto> CustomFields { get; set; } = new List<CustomFieldDto>();
    
}