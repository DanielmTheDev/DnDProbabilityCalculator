using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.AspNetCore.Components;

namespace DnDProbabilityCalculator.Blazor.PartyDisplay;

public partial class PartyCard
{
    [Parameter]
    public Party Party { get; set; } = null!;

    [Parameter]
    public EventCallback OnDelete { get; set; }

    [Parameter]
    public bool IsBusy { get; set; } = false;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    private void NavigateToParty(string partyId)
        => NavigationManager.NavigateTo($"/probability-calculator/{partyId}");

    private Task Delete()
    {
        IsBusy = true;
        return OnDelete.InvokeAsync();
    }
}