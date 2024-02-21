using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using DnDProbabilityCalculator.Shared.Party;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDProbabilityCalculator.Blazor.Pages.Party;

public partial class CreateParty
{
    private readonly CreatePartyDto _party = new();

    protected override void OnInitialized()
    {
        if (!_party.Characters.Any())
        {
            _party.Characters.Add(new());
        }
    }

    private static void SubmitValidForm()
    {
        Console.Out.Write("Valid Submit");
    }

    private IEnumerable<Option<AbilityScoreType>> GetAbilityTypeOptions()
        => Enum.GetValues<AbilityScoreType>().Select(value => new Option<AbilityScoreType>
        {
            Value = value,
            Text = value
        });
}