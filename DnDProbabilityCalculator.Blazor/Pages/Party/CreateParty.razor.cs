using DnDProbabilityCalculator.Shared.Party;

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
}