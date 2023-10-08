namespace DnDProbabilityCalculator.Application.Probabilities;

public record ProbabilityTableData
{
    public required string ActorName { get; set; }
    public required ICollection<int> DCs { get; set; }
    public required IEnumerable<double> StrengthProbabilities { get; set; }
    public required IEnumerable<double> DexterityProbabilities { get; set; }
    public required IEnumerable<double> ConstitutionProbabilities { get; set; }
    public required IEnumerable<double> WisdomProbabilities { get; set; }
    public required IEnumerable<double> IntelligenceProbabilities { get; set; }
    public required IEnumerable<double> CharismaProbabilities { get; set; }

    public List<IEnumerable<double>> AggregatedProbabilities
        => new()
        {
            StrengthProbabilities,
            DexterityProbabilities,
            ConstitutionProbabilities,
            WisdomProbabilities,
            IntelligenceProbabilities,
            CharismaProbabilities
        };
}