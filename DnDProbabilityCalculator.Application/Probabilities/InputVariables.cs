namespace DnDProbabilityCalculator.Application.Probabilities;

public record InputVariables
{
    public required int[] Dcs { get; init; }
    public required int[] AttackModifiers { get; init; }
    public required int NumberOfAttacks { get; init; }
}