namespace DnDProbabilityCalculator.Application.Table;

public record DCProbability
{
    public required int DC { get; set; }
    public required double StrengthProbability { get; set; }
    public required double DexterityProbability { get; set; }
    public required double ConstitutionProbability { get; set; }
    public required double WisdomProbability { get; set; }
    public required double IntelligenceProbability { get; set; }
    public required double CharismaProbability { get; set; }
}