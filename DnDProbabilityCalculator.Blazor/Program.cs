using DnDProbabilityCalculator.Application.Table;
using DnDProbabilityCalculator.Blazor;
using DnDProbabilityCalculator.Blazor.Configuration;
using DnDProbabilityCalculator.Blazor.PartyManipulation;
using DnDProbabilityCalculator.Core;
using DnDProbabilityCalculator.Infrastructure.Actors;
using DnDProbabilityCalculator.Infrastructure.FileSystem;
using DnDProbabilityCalculator.Shared.PartyCreation.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using Serilog;
using Serilog.Events;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddFluentUIComponents();
builder.Services.AddHotKeys2();

builder.Services.AddTransient<IPartyRepository, PartyInlineRepository>();
builder.Services.AddTransient<ITableContextFactory, TableContextFactory>();
builder.Services.AddScoped<IFileAccessor, FileAccessor>();
builder.Services.AddScoped<IPartyClient, PartyClient>();

var apiConfig = builder.Configuration.GetSection("Api").Get<ApiSettings>()!;

builder.Services.AddScoped<AuthorizationMessageHandler>(provider
    => new AuthorizationMessageHandler(provider.GetRequiredService<IAccessTokenProvider>(), provider.GetRequiredService<NavigationManager>())
        .ConfigureHandler(authorizedUrls: [apiConfig.BaseAddress], scopes: [apiConfig.Scope]));

Log.Logger = new LoggerConfiguration()
    .WriteTo.BrowserConsole(LogEventLevel.Debug)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Services.AddLogging(lb => lb.AddSerilog());

builder.Services
    .AddHttpClient<IPartyClient, PartyClient>(client => client.BaseAddress = new(apiConfig.BaseAddress))
    .AddHttpMessageHandler<AuthorizationMessageHandler>();

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add(apiConfig.Scope);
});

builder.Services.AddValidatorsFromAssemblyContaining<CreatePartyDtoValidator>();

await builder.Build().RunAsync();