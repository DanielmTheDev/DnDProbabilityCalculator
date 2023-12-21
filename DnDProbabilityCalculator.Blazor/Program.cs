using DnDProbabilityCalculator.Application.Table;
using DnDProbabilityCalculator.Core;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using DnDProbabilityCalculator.Blazor;
using DnDProbabilityCalculator.Infrastructure.Actors;
using DnDProbabilityCalculator.Infrastructure.FileSystem;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new(builder.HostEnvironment.BaseAddress) });
builder.Services.AddFluentUIComponents();

builder.Services.AddTransient<IPartyRepository, PartyInlineRepository>();
builder.Services.AddTransient<ITableContextFactory, TableContextFactory>();
builder.Services.AddScoped<IFileAccessor, FileAccessor>();

await builder.Build().RunAsync();