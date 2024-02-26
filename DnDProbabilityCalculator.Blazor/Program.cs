using DnDProbabilityCalculator.Application.Table;
using DnDProbabilityCalculator.Core;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using DnDProbabilityCalculator.Blazor;
using DnDProbabilityCalculator.Blazor.PartyCreation;
using DnDProbabilityCalculator.Infrastructure.Actors;
using DnDProbabilityCalculator.Infrastructure.FileSystem;
using DnDProbabilityCalculator.Shared.PartyCreation.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new(builder.HostEnvironment.BaseAddress) });
builder.Services.AddFluentUIComponents();
builder.Services.AddHotKeys2();

builder.Services.AddTransient<IPartyRepository, PartyInlineRepository>();
builder.Services.AddTransient<ITableContextFactory, TableContextFactory>();
builder.Services.AddScoped<IFileAccessor, FileAccessor>();
builder.Services.AddScoped<IPartySaver, PartySaver>();

builder.Services.AddScoped<AuthorizationMessageHandler>(provider
    => new AuthorizationMessageHandler(provider.GetRequiredService<IAccessTokenProvider>(), provider.GetRequiredService<NavigationManager>())
        .ConfigureHandler(authorizedUrls: ["https://dnd-probability-calculator-functions.azurewebsites.net"],
            scopes: ["https://dadamucki.onmicrosoft.com/a2cfc276-854c-44fc-b7aa-5706508ed32c/API.Access"]));

builder.Services
    // .AddHttpClient("B2CSandbox.ServerAPI", client => client.BaseAddress = new("https://dnd-probability-calculator-functions.azurewebsites.net"))
    .AddHttpClient("B2CSandbox.ServerAPI", client => client.BaseAddress = new("http://localhost:7071")) // for local
    .AddHttpMessageHandler<AuthorizationMessageHandler>();

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://dadamucki.onmicrosoft.com/a2cfc276-854c-44fc-b7aa-5706508ed32c/API.Access");
});

builder.Services.AddValidatorsFromAssemblyContaining<CreatePartyDtoValidator>();

await builder.Build().RunAsync();