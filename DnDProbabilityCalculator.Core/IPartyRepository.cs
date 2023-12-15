using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Core;

public interface IPartyRepository
{
    Party Get();
}