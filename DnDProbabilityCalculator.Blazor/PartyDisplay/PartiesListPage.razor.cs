using DnDProbabilityCalculator.Blazor.PartyCreation;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.AspNetCore.Components;

namespace DnDProbabilityCalculator.Blazor.PartyDisplay;

public partial class PartiesListPage
{
    private Party[] _parties = [];

    [Inject]
    public IPartyClient PartyClient { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var result = await PartyClient.GetAll();
        _parties = result.IsSuccess
            ? result.Value
            : [];
    }
}