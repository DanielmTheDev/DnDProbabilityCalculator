using DnDProbabilityCalculator.Shared.PartyCreation;
using Microsoft.AspNetCore.Components;

namespace DnDProbabilityCalculator.Blazor.PartyCreation;

public partial class CreatePartyPage
{
    [Inject]
    public IPartySaver PartySaver { get; set; } = null!;

    private string _result = "Nothing yet";
    private readonly CreatePartyDto _party = new();

    protected override void OnInitialized()
    {
        if (_party.Characters.Count == 0)
        {
            _party.Characters.Add(new());
        }
    }
    private async Task Submit()
        => _result = await PartySaver.Save(_party);
}