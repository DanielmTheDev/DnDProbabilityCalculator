using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.AspNetCore.Components;

namespace DnDProbabilityCalculator.Blazor.PartyDisplay;

public partial class PartyCard
{
    [Parameter]
    public Party Party { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    private void NavigateToParty(string partyId)
        => NavigationManager.NavigateTo($"/party/{partyId}");
}