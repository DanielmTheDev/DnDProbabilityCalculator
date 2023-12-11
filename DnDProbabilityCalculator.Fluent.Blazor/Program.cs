using DnDProbabilityCalculator.Blazor.Application;
using DnDProbabilityCalculator.Core;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using DnDProbabilityCalculator.Fluent.Blazor;
using DnDProbabilityCalculator.Infrastructure.Actors;
using DnDProbabilityCalculator.Infrastructure.FileSystem;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddFluentUIComponents();

builder.Services.AddTransient<IPartyRepository, PartyFileRepository>();
builder.Services.AddTransient<IPartyProvider, PartyProvider>();
builder.Services.AddScoped<IFileAccessor, FileAccessor>();

await builder.Build().RunAsync();