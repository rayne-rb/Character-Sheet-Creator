using CharacterSheetCreator.Features.CustomSheetCreation.Models;
using CharacterSheetCreator.Shared;
using CharacterSheetCreator.Shared.Utilities;
using OneOf;

namespace CharacterSheetCreator.Features.CustomSheetCreation.Services;

public class CustomSheetService : ICustomSheetService
{
    public async Task<OneOf<List<CustomSheetDto>, AppError>> GetCustomSheets()
    {
        var customSheet = new CustomSheetDto();
        var customSheets = new List<CustomSheetDto>();
        try
        {
            customSheets.Add(customSheet);
        }
        catch (Exception e)
        {
            return new AppError(e.Message);
        }

        return customSheets;
    }
}