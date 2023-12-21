namespace DnDProbabilityCalculator.Application.Table.ReceiveHit;

public record ReceiveHitTableRow
{
    public required int NumberOfHits { get; init; }
    public required List<double> Cells { get; init; }
}