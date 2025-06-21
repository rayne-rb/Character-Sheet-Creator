using System.Data;
using CharacterSheetCreator.Client.Pages;
using CharacterSheetCreator.Data.Services;
using CharacterSheetCreator.Features;
using CharacterSheetCreator.Shared.Utilities;
using MudBlazor.Services;
using RepoDb;

var builder = WebApplication.CreateBuilder(args);

GlobalConfiguration
    .Setup(new()
    {
        EnumDefaultDatabaseType = DbType.Int32,
        
    })
    .UsePostgreSql();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionFactory = new PgSqlDataSource(connectionString);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddMudServices();
builder.Services.AddSingleton<IPgSqlDataSource>(provider => connectionFactory);

var moduleInitializers = new ModuleInitializers();
moduleInitializers.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(CharacterSheetCreator.Client._Imports).Assembly);

app.Run();