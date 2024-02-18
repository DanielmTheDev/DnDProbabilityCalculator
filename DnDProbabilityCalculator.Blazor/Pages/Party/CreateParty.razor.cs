using DnDProbabilityCalculator.Shared.Party;

namespace DnDProbabilityCalculator.Blazor.Pages.Party;

public partial class CreateParty
{
    private readonly CreatePartyDto _party = new();

    private static void SubmitValidForm()
    {
        Console.Out.Write("Valid Submit");
    }
}