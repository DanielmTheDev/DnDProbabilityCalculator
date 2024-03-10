using DnDProbabilityCalculator.Blazor.PartyCreation;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDProbabilityCalculator.Blazor.PartyDisplay;

public partial class PartiesListPage
{
    private Party[] _parties = [];

    [Inject]
    public IPartyClient PartyClient { get; set; } = null!;

    [Inject]
    public IToastService ToastService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var result = await PartyClient.GetAll();
        _parties = result.IsSuccess
            ? result.Value
            : [];
    }

    private async Task DeleteParty(Party party)
    {
        var result = await PartyClient.Delete(party.Id);

        if (result.IsSuccess)
        {
            _parties = _parties
                .Where(p => p.Id != party.Id)
                .ToArray();
            ToastService.ShowToast(ToastIntent.Success, "Party deleted");
        }
        else
        {
            ToastService.ShowToast(ToastIntent.Error, result.Errors.First().Message);
        }
    }
}