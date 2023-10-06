namespace DnDProbabilityCalculator.Application.Probabilities;

public record ProbabilityTable
{
    public required string ActorName { get; set; }
    public required List<DCProbability> DCs { get; set; }
}