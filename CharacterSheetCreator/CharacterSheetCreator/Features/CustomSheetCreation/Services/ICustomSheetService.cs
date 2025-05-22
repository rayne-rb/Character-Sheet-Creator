using CharacterSheetCreator.Features.CustomSheetCreation.Models;
using CharacterSheetCreator.Shared;
using CharacterSheetCreator.Shared.Utilities;
using OneOf;

namespace CharacterSheetCreator.Features.CustomSheetCreation.Services;

public interface ICustomSheetService
{
    Task<OneOf<List<CustomSheetDto>, AppError>> GetCustomSheets();
}