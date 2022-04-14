using Blazored.Toast;
using BlazorWithFluxor.Client;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("BlazorWithFluxor.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorWithFluxor.ServerAPI"));

builder.Services.AddApiAuthorization();

builder.Services.AddBlazoredToast();

// Add Fluxor
builder.Services.AddFluxor(options =>
{
    options
        .ScanAssemblies(typeof(Program).Assembly)
        // For ReduxDevTool support. Don't have this in Production
        .UseReduxDevTools();
});


await builder.Build().RunAsync();
