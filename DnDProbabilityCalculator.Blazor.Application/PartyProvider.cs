using DnDProbabilityCalculator.Core;
using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Blazor.Application;

public class PartyProvider(IPartyRepository partyRepository) : IPartyProvider
{
    public Party Get() => partyRepository.Get();
}