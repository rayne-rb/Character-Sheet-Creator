namespace CharacterSheetCreator.Shared.Interfaces;

public interface IModuleInitializer
{
    void ConfigureServices(IServiceCollection services, IConfiguration configuration);
}