using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WeightPlatesCalculator.Web;
using WeightPlatesCalculatorLibrary.Processors;
using WeightPlatesCalculatorLibrary.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddTransient<IWeightPlatesService, WeightPlatesService>();
builder.Services.AddTransient<IWeightPlatesProcessor, WeightPlatesProcessor>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


await builder.Build().RunAsync();
