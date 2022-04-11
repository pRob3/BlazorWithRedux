using BlazorWithRedux;
using Fluxor;
//using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add Fluxor
builder.Services.AddFluxor(options =>
{
    options
        .ScanAssemblies(typeof(Program).Assembly)
        // For ReduxDevTool support. Don't have this in Production
        .UseReduxDevTools();
});

await builder.Build().RunAsync();
