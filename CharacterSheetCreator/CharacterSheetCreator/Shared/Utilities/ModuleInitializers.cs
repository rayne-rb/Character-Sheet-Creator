using CharacterSheetCreator.Features.AttributeCreation;
using CharacterSheetCreator.Features.CustomSheetCreation;
using CharacterSheetCreator.Shared.Interfaces;

namespace CharacterSheetCreator.Shared.Utilities;

public class ModuleInitializers : IModuleInitializer
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var customSheetModuleInitializer = new CustomSheetModuleInitializer();
        customSheetModuleInitializer.ConfigureServices(services, configuration);

        var attributesModuleInitializer = new AttributesModuleInitializer();
        attributesModuleInitializer.ConfigureServices(services, configuration);
    }
}