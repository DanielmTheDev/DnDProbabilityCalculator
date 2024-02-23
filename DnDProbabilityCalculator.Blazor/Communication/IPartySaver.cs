using DnDProbabilityCalculator.Shared.PartyCreation;

namespace DnDProbabilityCalculator.Blazor.Communication;

public interface IPartySaver
{
    Task<string> Save(CreatePartyDto party);
}