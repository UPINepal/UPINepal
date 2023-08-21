using Client.seo;
using Server.Components;
using Server.Implementations;
using Shared;
using Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents()
    .AddServerComponents()
    .AddWebAssemblyComponents();
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ThemeState>();
builder.Services.AddSingleton<MetadataProvider>();
builder.Services.AddScoped<MetadataTransferService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapGet("/Endpoint/banks",
async (IDataService dataService,
            CancellationToken ct) =>
        {

            var banks = await dataService.GetBanks();
            return Results.Ok(banks);
        }).WithName("banks")
    ;
app.MapGet("/Endpoint/apps",
async (IDataService dataService,
            CancellationToken ct) =>
{
    var apps = await dataService.GetApps();
    return Results.Ok(apps);
}).WithName("apps");

app.MapRazorComponents<App>()
    .AddServerRenderMode()
    .AddWebAssemblyRenderMode();

app.Run();
