using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VivesBlog.Blazor;
using MudBlazor.Services;
using Vives.Security;
using VivesBlog.Blazor.Stores;
using VivesBlog.Blazor.Settings;
using VivesBlog.Sdk.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddScoped<ITokenStore, TokenStore>();

var apiSettings = builder.Configuration.GetSection(nameof(ApiSettings)).Get<ApiSettings>();
if (apiSettings is null)
{
    throw new InvalidOperationException($"Configuration section '{nameof(ApiSettings)}' is missing or invalid.");
}
builder.Services.InstallApi(apiSettings.BaseUrl);

await builder.Build().RunAsync();
