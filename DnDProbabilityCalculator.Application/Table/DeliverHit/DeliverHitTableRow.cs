namespace DnDProbabilityCalculator.Application.Table.DeliverHit;

public record DeliverHitTableRow
{
    public required int NumberOfHits { get; init; }
    public required List<double> Cells { get; init; }
}