using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using DnDProbabilityCalculator.Shared.PartyCreation;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDProbabilityCalculator.Blazor.PartyCreation;

public partial class CreateCharacterCard
{
    [Parameter]
    public CreateCharacterDto Character { get; set; } = null!;

    private IEnumerable<Option<AbilityScoreType>> GetAbilityTypeOptions()
        => Enum.GetValues<AbilityScoreType>().Select(value => new Option<AbilityScoreType>
        {
            Value = value,
            Text = value
        });
}