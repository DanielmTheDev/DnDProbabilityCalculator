using DnDProbabilityCalculator.Core.Adventuring.Abilities;

namespace DnDProbabilityCalculator.Application.Table.SavingThrow;

public record SavingThrowTableRow
{
    public required AbilityScoreType AbilityScoreType { get; init; }
    public required List<double> Cells { get; init; }
    public required bool IsProficient { get; init; }
    public required int AbilityScore { get; init; }
}