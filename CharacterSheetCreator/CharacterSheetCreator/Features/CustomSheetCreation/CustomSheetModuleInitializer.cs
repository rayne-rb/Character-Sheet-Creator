using CharacterSheetCreator.Features.CustomSheetCreation.Services;
using CharacterSheetCreator.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CharacterSheetCreator.Features.CustomSheetCreation;

public class CustomSheetModuleInitializer : IModuleInitializer
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICustomSheetService, CustomSheetService>();
    }
}