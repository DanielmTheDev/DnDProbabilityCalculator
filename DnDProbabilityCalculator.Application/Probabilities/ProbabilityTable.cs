namespace DnDProbabilityCalculator.Application.Probabilities;

public record ProbabilityTable
{
    public required string ActorName { get; set; }
    public required List<DCProbability> DcProbabilities { get; set; }
}