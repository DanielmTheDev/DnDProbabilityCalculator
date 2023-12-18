namespace DnDProbabilityCalculator.Application.Table.Context;

public record DeliverHitTableRow
{
    public required int NumberOfHits { get; init; }
    public required List<double> Cells { get; init; }
}