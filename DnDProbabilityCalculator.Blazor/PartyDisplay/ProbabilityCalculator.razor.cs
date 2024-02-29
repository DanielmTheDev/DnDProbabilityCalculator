using DnDProbabilityCalculator.Blazor.PartyCreation;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.AspNetCore.Components;

namespace DnDProbabilityCalculator.Blazor.PartyDisplay;

public partial class ProbabilityCalculator
{
    private Party? _party;

    [Inject]
    public IPartyClient PartyClient { get; set; } = null!;

    [Parameter]
    public string PartyId { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        var result = await PartyClient.Get(PartyId);

        if (result.IsSuccess)
        {
            _party = result.Value;
        }
        else
        {
            await Console.Out.WriteLineAsync(result.Errors.First().Message);
        }
    }
}