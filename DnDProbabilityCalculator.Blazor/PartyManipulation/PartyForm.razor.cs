using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Shared.PartyCreation;
using Microsoft.AspNetCore.Components;

namespace DnDProbabilityCalculator.Blazor.PartyManipulation;

public partial class PartyForm
{
    [Parameter]
    public CreatePartyDto Party { get; set; } = new();

    [Parameter]
    public bool IsFormDisabled { get; set; }

    [Parameter]
    public EventCallback<CreatePartyDto> SubmitCallback { get; set; }
}