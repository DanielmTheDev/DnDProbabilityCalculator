using DnDProbabilityCalculator.Shared.PartyCreation;

namespace DnDProbabilityCalculator.Blazor.PartyCreation;

public interface IPartySaver
{
    Task<string> Save(CreatePartyDto party);
}