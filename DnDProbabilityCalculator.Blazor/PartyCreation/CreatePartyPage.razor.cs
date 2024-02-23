using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using DnDProbabilityCalculator.Shared.PartyCreation;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

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
    private IEnumerable<Option<AbilityScoreType>> GetAbilityTypeOptions()
        => Enum.GetValues<AbilityScoreType>().Select(value => new Option<AbilityScoreType>
        {
            Value = value,
            Text = value
        });

    private async Task Submit()
        => _result = await PartySaver.Save(_party);
}