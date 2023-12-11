using System.Text.Json;
using DnDProbabilityCalculator.Blazor.Application;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.AspNetCore.Components;

namespace DnDProbabilityCalculator.Fluent.Blazor.Pages;

public partial class Home
{
    [Inject]
    private  IPartyProvider partyProvider { get; set; }

    private Party _party = new();
    private string _partyAsJson => JsonSerializer.Serialize(_party);

    protected override void OnInitialized()
        => _party = partyProvider.Get();
}