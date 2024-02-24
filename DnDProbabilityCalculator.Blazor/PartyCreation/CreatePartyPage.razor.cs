using DnDProbabilityCalculator.Shared.PartyCreation;
using Microsoft.AspNetCore.Components;

namespace DnDProbabilityCalculator.Blazor.PartyCreation;

public partial class CreatePartyPage
{
    [Inject]
    public IPartySaver PartySaver { get; set; } = null!;

    private string _result = "Nothing yet";
    private bool _isFormDisabled;
    private CreatePartyDto _party = new() { Characters = [new()] };

    private async Task Submit()
    {
        _isFormDisabled = true;
        _result = await PartySaver.Save(_party);
        _party = new() { Characters = [new()] };
        _isFormDisabled = false;
    }
}