using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using DnDProbabilityCalculator.Shared.PartyCreation;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDProbabilityCalculator.Blazor.PartyManipulation;

public partial class CreateCharacterCard
{
    [Parameter]
    public CreateCharacterDto Character { get; set; } = null!;

    [Parameter]
    public bool AreInputsDisabled { get; set; }

    [Parameter]
    public EventCallback Remove { get; set; }

    private static IEnumerable<Option<AbilityScoreType>> GetAbilityTypeOptions()
        => Enum.GetValues<AbilityScoreType>().Select(value => new Option<AbilityScoreType>
        {
            Value = value,
            Text = value
        });
}