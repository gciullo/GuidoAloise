using GuidoAloise;
using GuidoAloise.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


// Localization
var cultureDefault = new CultureInfo("it-IT");
var supportedCultures = new List<CultureInfo> {
        cultureDefault,
        new("en-GB"),
    };


builder.Services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(cultureDefault);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://guidoaloise-api.onrender.com/") // URL della tua Web API
});
builder.Services.AddScoped<IDataService, DataService>();

var host = builder.Build();


// Legge la lingua dal cookie o usa default
var js = host.Services.GetRequiredService<IJSRuntime>();
var culture = await js.InvokeAsync<string>("blazorCulture.get");
if (!string.IsNullOrWhiteSpace(culture))
{
    CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
    CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(culture);
}
else
{
    CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("it-IT");
    CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("it-IT");
}

await host.RunAsync();
