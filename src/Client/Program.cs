using Client.Implementations;
using Client.Interfaces;
using Client.seo;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Refit;
using Shared;
using Shared.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.Services.AddSingleton<MetadataProvider>();
builder.Services.AddScoped<MetadataTransferService>();
builder.Services.AddScoped<ThemeState>();
builder.Services.AddScoped<IMenuService,MenuService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

builder.Services.AddScoped<IDataService, Client.Implementations.DataService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddRefitClient<IRefitDataService>()
    .ConfigureHttpClient(c => { c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); });
await builder.Build().RunAsync();
