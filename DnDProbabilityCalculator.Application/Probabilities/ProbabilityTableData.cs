namespace DnDProbabilityCalculator.Application.Probabilities;

public record ProbabilityTableData
{
    public required List<string> HeaderRow { get; set; }
    public required List<string> StrengthRow { get; set; }
    public required List<string> DexterityRow { get; set; }
    public required List<string> ConstitutionRow { get; set; }
    public required List<string> WisdomRow { get; set; }
    public required List<string> IntelligenceRow { get; set; }
    public required List<string> CharismaRow { get; set; }

    public List<List<string>> AllRows
        => new()
        {
            StrengthRow,
            DexterityRow,
            ConstitutionRow,
            WisdomRow,
            IntelligenceRow,
            CharismaRow
        };
}