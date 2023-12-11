using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Blazor.Application;

public interface IPartyProvider
{
    Party Get();
}