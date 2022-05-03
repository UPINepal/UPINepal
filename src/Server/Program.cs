using Client.seo;
using Microsoft.AspNetCore.Components;
using Radzen;
using Server.Implementations;
using Shared;
using Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IDataService, DataService>();
builder.Services.AddScoped<HttpClient>(s =>
{
    NavigationManager navigationManager=null;
    navigationManager = s.GetRequiredService<NavigationManager>();
    return new HttpClient
    {
        BaseAddress = new Uri(navigationManager.BaseUri)
    };
});
builder.Services.AddScoped<ThemeState>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddSingleton<MetadataProvider>();
builder.Services.AddScoped<MetadataTransferService>();

var app = builder.Build();

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

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToPage("/_Host");

app.Run();
