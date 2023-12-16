using System.Text.Json;
using DnDProbabilityCalculator.Blazor.Application;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.AspNetCore.Components;

namespace DnDProbabilityCalculator.Fluent.Blazor.Pages;

public partial class Home
{
    [Inject]
    private IPartyProvider PartyProvider { get; set; } = null!;
    private Party _party = new();
    private string PartyAsJson => JsonSerializer.Serialize(_party);

    protected override void OnInitialized()
        => _party = PartyProvider.Get();
}