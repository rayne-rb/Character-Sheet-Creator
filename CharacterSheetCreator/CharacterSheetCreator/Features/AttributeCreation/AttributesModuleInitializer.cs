using CharacterSheetCreator.Features.AttributeCreation.Repositories;
using CharacterSheetCreator.Shared.Interfaces;

namespace CharacterSheetCreator.Features.AttributeCreation;

public class AttributesModuleInitializer : IModuleInitializer
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAttributesRepository, AttributesRepository>();
    }
}