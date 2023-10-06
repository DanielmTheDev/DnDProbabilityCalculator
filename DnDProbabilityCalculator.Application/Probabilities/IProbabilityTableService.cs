using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Probabilities;

public interface IProbabilityTableService
{
    Party Get();
}